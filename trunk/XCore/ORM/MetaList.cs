//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\MetaList.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Text;
namespace System.ORM {
    /// <summary>
    /// Ԫ�����б�
    /// </summary>
    public class MetaList {
        public MetaList( IDictionary asmList, IDictionary clsList ) {
            this.AssemblyList = asmList;
            this.ClassList = clsList;
        }
		private IDictionary _AssemblyList;
		public IDictionary AssemblyList { get{return _AssemblyList;} set{_AssemblyList=value;}  }
		private IDictionary _ClassList;
		public IDictionary ClassList { get{return _ClassList;} set{_ClassList=value;}  }
    }
}