/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Common
*项目描述:
*类 名 称:Module
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:14:01
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/

namespace FrameworkCore.ViewModel.Common
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    public class Module : ObservableObject
    {
        private string code;
        private string typeName;
        private string name;
        private int auth;

        /// <summary>
        /// 模块代码
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 模块权限
        /// </summary>
        public int Auth
        {
            get { return auth; }
            set { auth = value; OnPropertyChanged(); }
        }
    }

    /// <summary>
    /// 模块UI组件
    /// </summary>
    public class ModuleUIComponent : Module
    {
        private object body;

        public object Body
        {
            get { return body; }
            set { body = value; OnPropertyChanged(); }
        }
    }
}
