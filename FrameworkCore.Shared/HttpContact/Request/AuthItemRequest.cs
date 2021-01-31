/*****************************************************************************
*项目名称:FrameworkCore.Shared.HttpContact.Request
*项目描述:
*类 名 称:AuthItemRequest
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:19:27
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.HttpContact.Request
{
    public class AuthItemRequest : BaseRequest
    {
        public override string Route { get => "api/AuthItem/GetAll"; }
    }
}
