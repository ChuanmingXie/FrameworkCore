/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:MenuService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:03:45
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Service
{
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.Dto;


    public class MenuService : BaseService<MenuDto>, IMenuRepository
    {

    }
}
