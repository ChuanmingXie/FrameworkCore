/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Interfaces
*项目描述:
*类 名 称:IBaseCenter
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:11:15
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.ViewModel.Interfaces
{
    public interface IBaseCenter
    {
        /// <summary>
        /// 关联默认参数上下文
        /// </summary>
        void BindDefaultModel();

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        object GetView();

        /// <summary>
        /// 关联默认参数上下文(包括权限相关)
        /// </summary>
        /// <param name="AuthValue"></param>
        /// <returns></returns>
        Task BindDefaultModel(int AuthValue = 0);

        /// <summary>
        /// 关联表格列
        /// </summary>
        void BindDataGridColums();
    }
}
