/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Interfaces
*项目描述:
*类 名 称:IOrdinary
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:11:51
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.ViewModel.Interfaces
{
    using Microsoft.Toolkit.Mvvm.Input;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IOrdinary<TEntity> where TEntity : class
    {
        /// <summary>
        /// 选中表单
        /// </summary>
        TEntity GridModel { get; set; }

        /// <summary>
        /// 页索引
        /// </summary>
        int SelectPageIndex { get; set; }

        /// <summary>
        /// 搜素参数
        /// </summary>
        string Search { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        ObservableCollection<TEntity> GridModelList { get; set; }

        /// <summary>
        /// 搜素命令
        /// </summary>
        AsyncRelayCommand QueryCommand { get; }

        /// <summary>
        /// 其他命令
        /// </summary>
        AsyncRelayCommand<string> ExecuteCommand { get; }

        /// <summary>
        /// 添加
        /// </summary>
        void AddAsync();

        /// <summary>
        /// 更新
        /// </summary>
        void UpdateAsync();

        /// <summary>
        /// 更新
        /// </summary>
        Task DeleteAsync();

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// 取消
        /// </summary>
        void Cancel();
    }
}
