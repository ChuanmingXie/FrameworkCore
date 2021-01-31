/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:BasicService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 21:28:08
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using FrameworkCore.Shared.DataInterfaces;
using FrameworkCore.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Service
{
    public class BasicService : BaseService<BasicDto>, IBasicRepository
    {
    }
}
