/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataInterfaces
*项目描述:
*类 名 称:IDataPager
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:58:47
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.DataInterfaces
{
    using System.Threading.Tasks;
    public interface IDataPager
    {
        #region properties

        /// <summary>
        /// 总行数
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        int PageCount { get; set; }

        #endregion

        #region Task

        /// <summary>
        /// 返回首页任务
        /// </summary>
        /// <returns></returns>
        Task GoHomePage();

        /// <summary>
        /// 去上一页
        /// </summary>
        /// <returns></returns>
        Task GoOnPage();

        /// <summary>
        /// 去下一页
        /// </summary>
        /// <returns></returns>
        Task GoNextPage();

        /// <summary>
        /// 去尾页
        /// </summary>
        /// <returns></returns>
        Task GoEndPage();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task GetPageData(int pageIndex);

        #endregion
    }
}
