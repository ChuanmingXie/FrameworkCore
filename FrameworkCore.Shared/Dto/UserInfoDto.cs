/*****************************************************************************
*项目名称:FrameworkCore.Shared.Dto
*项目描述:
*类 名 称:UserInfoDto
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:30:43
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Dto
{
    using FrameworkCore.Shared.DataModel;
    using System.Collections.Generic;

    public class UserInfoDto
    {
        public User User { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
