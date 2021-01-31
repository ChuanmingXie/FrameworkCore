/*****************************************************************************
*项目名称:FrameworkCore.Shared.HttpContact
*项目描述:
*类 名 称:BaseRequest
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:16:51
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.HttpContact
{
    using FrameworkCore.Shared.Common.Attributes;
    using Newtonsoft.Json;
    using System;
    using System.Reflection;
    using System.Text;
    using System.Web;

    /// <summary>
    /// 请求参数的构造基类
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// 路由地址
        /// </summary>
        [Prevent]
        public virtual string Route { get; set; }

        /// <summary>
        /// 参数获取
        /// </summary>
        [Prevent]
        public string getParameter { get; set; }

        /// <summary>
        /// 解析对象参数
        /// </summary>
        /// <returns></returns>
        public string GetPropertiesObject()
        {
            /* 定义一个内部函数,返回不同的处理类型 */
            Func<object, string> CaseState = (pvalue) =>
            {
                var strNameSpace = pvalue.GetType().Namespace;
                if (pvalue != null && strNameSpace == "Consumption.Shared.Common.Query")
                    return "CommonQuery";
                if (pvalue != null && strNameSpace.Contains("Consumption.Shared.Dto"))
                    return "SharedDTO";
                if (pvalue != null && strNameSpace.Contains("Consumption.Shared.HttpContact"))
                    return "SharedHttpContact";
                return "OtherBase";
            };

            StringBuilder getBuilder = new StringBuilder();
            StringBuilder builder = new StringBuilder();
            var type = this.GetType();
            var propertyArray = type.GetProperties();
            if (propertyArray != null && propertyArray.Length > 0)
            {
                foreach (var property in propertyArray)
                {
                    var prevent = property.GetCustomAttribute<PreventAttribute>();
                    if (prevent != null) continue;

                    var pvalue = property.GetValue(this);
                    if (pvalue == null) continue;
                    var strName = pvalue.GetType().Namespace;

                    switch (CaseState(pvalue))
                    {
                        case "CommonQuery": JointCommonQuery(getBuilder, pvalue); break;
                        case "SharedDTO":
                        case "SharedHttpContact": JointDTOAndHttp(builder, pvalue); break;
                        case "OtherBase": JointOtherBase(property, getBuilder, pvalue); break;
                    }
                }
            }
            string getStr = getBuilder.ToString().Trim('&');
            if (!string.IsNullOrWhiteSpace(getStr))
                getParameter = getStr;
            return builder.ToString().Trim('&');
        }

        /// <summary>
        /// 当属性为C#基础类的情况下,默认get传参,拼接至路由地址中
        /// </summary>
        /// <param name="property"></param>
        /// <param name="getBuilder"></param>
        /// <param name="pvalue"></param>
        private void JointOtherBase(PropertyInfo property, StringBuilder getBuilder, object pvalue)
        {
            if (getBuilder.ToString() == string.Empty)
                getBuilder.Append("?");
            getBuilder.Append($"{property.Name}={HttpUtility.UrlEncode(Convert.ToString(pvalue))}&");
        }

        /// <summary>
        /// 当属性为对象的情况下,进项序列化
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pvalue"></param>
        private void JointDTOAndHttp(StringBuilder builder, object pvalue)
        {
            pvalue = JsonConvert.SerializeObject(pvalue);
            builder.Append(pvalue);
        }

        /// <summary>
        /// 当参数为Query类型时,则进项拆解对象拼接字符串
        /// </summary>
        /// <param name="getBuilder"></param>
        /// <param name="pvalue"></param>
        private void JointCommonQuery(StringBuilder getBuilder, object pvalue)
        {
            StringBuilder pbuilder = new StringBuilder();
            var QpropertyArray = pvalue.GetType().GetProperties();
            if (QpropertyArray != null && QpropertyArray.Length > 0)
            {
                foreach (var Qproperty in QpropertyArray)
                {
                    var Qprevent = Qproperty.GetCustomAttribute<PreventAttribute>();
                    if (Qprevent != null) continue;
                    var Qpvalue = Qproperty.GetValue(pvalue);
                    if (Qpvalue != null && Qpvalue.ToString() != "")
                    {
                        if (getBuilder.ToString() == string.Empty)
                            getBuilder.Append("?");
                        getBuilder.Append(Qproperty.Name + "=" + HttpUtility.UrlEncode(Convert.ToString(Qpvalue)) + "&");
                    }
                }
            }
            getBuilder.Append(pbuilder.ToString());
        }

        /// <summary>
        /// 获取请求API地址
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public string GetRouteUrl(string addr)
        {
            return Route += addr;
        }
    }
}
