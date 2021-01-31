/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:BaseRepository
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:18:45
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.ViewModel
{
    using FrameworkCore.Shared.Common;
    using FrameworkCore.Shared.Common.Aop;
    using FrameworkCore.Shared.Common.Query;
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.ViewModel.Common;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 通用基类(实现CRUD / 数据分页 / 权限验证)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : ObservableObject where TEntity : BaseDto, new()
    {
        public readonly IJobDesignerRepository<TEntity> repository;

        public BaseRepository()
        {

        }

        public BaseRepository(IJobDesignerRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        #region CRUD (增删改查)

        private int selectPageIndex;
        private string search;
        private TEntity gridModel;
        private ObservableCollection<TEntity> gridModelList;

        public TEntity GridModel
        {
            get { return gridModel; }
            set { gridModel = value; OnPropertyChanged(); }
        }

        public int SelectPageIndex
        {
            get { return selectPageIndex; }
            set { selectPageIndex = value; OnPropertyChanged(); }
        }

        public string Search
        {
            get { return search; }
            set { search = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TEntity> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; OnPropertyChanged(); }
        }

        public AsyncRelayCommand QueryCommand
        {
            get { return new AsyncRelayCommand(Query); }
        }

        public AsyncRelayCommand<string> ExecuteCommand
        {
            get { return new AsyncRelayCommand<string>(arg => Execute(arg)); }
        }

        public virtual async Task Query()
        {
            await GetPageData(this.PageIndex);
        }

        public virtual async Task Execute(string arg)
        {
            switch (arg)
            {
                case "添加": AddAsync(); break;
                case "修改": UpdateAsync(); break;
                case "删除": await DeleteAsync(); break;
                case "保存": await SaveAsync(); break;
                case "取消": Cancel(); break;
            }
        }

        public virtual void AddAsync()
        {
            this.CreateDefaultCommand();
            GridModel = new TEntity();
            SelectPageIndex = 1;
        }

        public virtual void Cancel()
        {
            InitPermissions(this.AuthValue);
            SelectPageIndex = 0;
        }

        public virtual async Task DeleteAsync()
        {
            if (GridModel != null)
            {
                if (await Msg.Question("确认删除当前选中行的数据"))
                {
                    var result = await repository.DeleteAsync(GridModel.Id);
                    if (result.StatusCode == 200)
                        await GetPageData(0);
                    else
                        WeakReferenceMessenger.Default.Send(result.Message, "Snackbar");
                }
            }
        }

        [GlobalProgress]
        public virtual async Task SaveAsync()
        {
            if (GridModel == null) return;
            await repository.SaveAsync(GridModel);
            InitPermissions(this.AuthValue);
            await GetPageData(0);
            SelectPageIndex = 0;
        }

        [GlobalProgress]
        public virtual async void UpdateAsync()
        {
            if (GridModel == null) return;
            var baseResponse = await repository.GetAsync(GridModel.Id);
            if (baseResponse.SatusCode == 200)
            {
                GridModel = baseResponse.Result;
                this.CreateDefaultCommand();
                SelectPageIndex = 1;
            }
            else
                WeakReferenceMessenger.Default.Send("Get data exception !", "Snackbar");
        }

        #endregion

        #region 数据分页

        private int totalCount = 0;
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; OnPropertyChanged(); }
        }

        private int pageSize = 30;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; OnPropertyChanged(); }
        }

        private int pageIndex = 0;
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; OnPropertyChanged(); }
        }

        private int pageCount = 0;
        public int PageCount
        {
            get { return PageCount; }
            set { pageCount = value; OnPropertyChanged(); }
        }

        public AsyncRelayCommand GoHomePageCommand
        {
            get { return new AsyncRelayCommand(GoHomePage); }
        }

        public AsyncRelayCommand GoOnPageCommand
        {
            get { return new AsyncRelayCommand(GoOnPage); }
        }

        public AsyncRelayCommand GoNextPageCommand
        {
            get { return new AsyncRelayCommand(GoNextPage); }
        }

        public AsyncRelayCommand GoEndPageCommand
        {
            get { return new AsyncRelayCommand(GoEndPage); }
        }

        public async virtual Task GoEndPage()
        {
            this.PageIndex = PageCount;
            await GetPageData(PageCount);
        }

        public virtual async Task GoNextPage()
        {
            if (this.PageIndex == PageCount) return;
            PageIndex++;
            await this.GetPageData(PageIndex);
        }

        public virtual async Task GoOnPage()
        {
            if (this.PageIndex == 0) return;
            PageIndex--;
            await this.GetPageData(PageIndex);
        }

        public virtual async Task GoHomePage()
        {
            if (this.PageIndex == 0) return;
            pageIndex = 0;
            await GetPageData(PageIndex);
        }

        public virtual async Task GetPageData(int pageIndex)
        {
            var result = await repository.GetAllListAsync(new QueryParameters()
            {
                PageIndex = this.PageIndex,
                PageSize = this.PageSize,
                Search = this.Search
            });
            if (result.SatusCode == 200)
            {
                GridModelList = new ObservableCollection<TEntity>(result.Result.Items.ToList());
                TotalCount = result.Result.TotalCount;
                SetPageCount();
            }
        }

        public virtual void SetPageCount()
        {
            PageCount = Convert.ToInt32(Math.Ceiling((double)TotalCount / (double)PageSize));
        }

        #endregion

        #region 权限内容

        /// <summary>
        /// 创建页面默认命令
        /// </summary>
        public void CreateDefaultCommand()
        {
            ToolBarCommandList.Clear();
            ToolBarCommandList.Add(new CommandStruct() { CommandName = "保存", CommandColor = "#0066FF", CommandKind = "ContentSave" });
            ToolBarCommandList.Add(new CommandStruct() { CommandName = "取消", CommandColor = "#FF6633", CommandKind = "Cancel" });
        }

        private ObservableCollection<CommandStruct> toolBarCommandList;

        public ObservableCollection<CommandStruct> ToolBarCommandList
        {
            get { return toolBarCommandList; }
            set { toolBarCommandList = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 页面权限值
        /// </summary>
        public int AuthValue { get; private set; }

        public void InitPermissions(int authValue)
        {
            this.AuthValue = authValue;
            ToolBarCommandList = new ObservableCollection<CommandStruct>();
            Contract.AuthItems.ForEach(arg =>
            {
                if ((authValue & arg.AuthValue) == arg.AuthValue)
                {
                    ToolBarCommandList.Add(new CommandStruct()
                    {
                        CommandName = arg.AuthName,
                        CommandColor = arg.AuthColor,
                        CommandKind = arg.AuthKind
                    });
                }
            });
        }

        #endregion
    }
}
