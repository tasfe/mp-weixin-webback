//------------------------------------------------------------------------------
//	文件名称：System\cvt.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Data;
namespace System {
    /// <summary>
    /// 不同类型之间数值转换
    /// </summary>
    public partial class cvt
    {
        /// <summary>
        /// 将对象序列化为 xml (内部调用 .net 框架自带的 XmlSerializer)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToXML( Object obj ) {
            return EasyDB.SaveToString( obj );
        }
    }
}