/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataInterfaces
*项目描述:
*类 名 称:IDataInitializer
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:58:36
*修 改 人:
*修改时间:
*作用描述:初始化数据
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/

namespace FrameworkCore.Shared.DataInterfaces
{
    using System.Threading.Tasks;
    public interface IDataInitializer
    {
        Task InitSampleDataAsync();
    }
}
