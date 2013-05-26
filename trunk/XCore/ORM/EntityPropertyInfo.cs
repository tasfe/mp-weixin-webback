//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\EntityPropertyInfo.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection;
namespace System.ORM {
    /// <summary>
    /// ʵ����ĳ�����Ե�Ԫ������Ϣ
    /// </summary>
    [Serializable]
    public partial class EntityPropertyInfo {
        public EntityPropertyInfo() {
            this.IsList = false;
            this.IsAbstractEntity = false;
            this.IsEntity = false;
            this.ValidationAttributes = new List<ValidationAttribute>();
        }
        private String _Name;
        /// <summary>
        /// ��������
        /// </summary>
        public String Name {get{return _Name;} set{_Name=value;} }
        private String _ColumnName;
        /// <summary>
        /// ��Ӧ����������
        /// </summary>
        public String ColumnName {get{return _ColumnName;} set{_ColumnName=value;} }
        private Type _Type;
        /// <summary>
        /// ���Ե����ͣ�������int������string�ȵ�
        /// </summary>
        public Type Type {get{return _Type;} set{_Type=value;} }
        private PropertyInfo _Property;
        /// <summary>
        /// ������Ϣ(ϵͳ�Դ���Ԫ����)
        /// </summary>
        public PropertyInfo Property {get{return _Property;} set{_Property=value;} }
        private EntityInfo _ParentEntityInfo;
        /// <summary>
        /// ������������ʵ������Ϣ������Blog��һ������Title����Title������Ե�ParentEntityInfo����Blog
        /// </summary>
        public EntityInfo ParentEntityInfo {get{return _ParentEntityInfo;} set{_ParentEntityInfo=value;} }
        private EntityInfo _EntityInfo;
        /// <summary>
        /// ����������ʵ������ʱ����ʵ�����Ե���Ϣ������Blog��ʵ������Category��EntityInfo���������ʵ�����ԣ���Ϊnull
        /// </summary>
        public EntityInfo EntityInfo {get{return _EntityInfo;} set{_EntityInfo=value;} }
        private Boolean _SaveToDB;
        /// <summary>
        /// �Ƿ񱣴浽���ݿ�(�Ƿ������NotSave��ע)
        /// </summary>
        public Boolean SaveToDB {get{return _SaveToDB;} set{_SaveToDB=value;} }
        private Boolean _IsList;
        /// <summary>
        /// �Ƿ����б����ͣ��б����Ͳ��ᱣ�浽���ݿ�
        /// </summary>
        public Boolean IsList {get{return _IsList;} set{_IsList=value;} }
        private Boolean _IsEntity;
        /// <summary>
        /// �Ƿ���ʵ��������
        /// </summary>
        public Boolean IsEntity {get{return _IsEntity;} set{_IsEntity=value;} }
        private Boolean _IsAbstractEntity;
        /// <summary>
        /// �Ƿ��ǳ�������ʵ��
        /// </summary>
        public Boolean IsAbstractEntity {get{return _IsAbstractEntity;} set{_IsAbstractEntity=value;} }
        private ColumnAttribute _SaveAttribute;
        /// <summary>
        /// ��ǰ���Ե� ColumnAttribute
        /// </summary>
        public ColumnAttribute SaveAttribute {get{return _SaveAttribute;} set{_SaveAttribute=value;} }
        private LongTextAttribute _LongTextAttribute;
        /// <summary>
        /// ��ǰ���Ե� LongTextAttribute
        /// </summary>
        public LongTextAttribute LongTextAttribute {get{return _LongTextAttribute;} set{_LongTextAttribute=value;} }
        private DateTimeAttribute _DateTimeAttribute;
        /// <summary>
        /// ��ǰ���Ե� DateTimeAttribute
        /// </summary>
        public DateTimeAttribute DateTimeAttribute {get{return _DateTimeAttribute;} set{_DateTimeAttribute=value;} }
        private MoneyAttribute _MoneyAttribute;
        /// <summary>
        /// ��ǰ���Ե� MoneyAttribute
        /// </summary>
        public MoneyAttribute MoneyAttribute {get{return _MoneyAttribute;} set{_MoneyAttribute=value;} }
        private DecimalAttribute _DecimalAttribute;
        /// <summary>
        /// ��ǰ���Ե� DecimalAttribute
        /// </summary>
        public DecimalAttribute DecimalAttribute {get{return _DecimalAttribute;} set{_DecimalAttribute=value;} }
        private DefaultAttribute _DefaultAttribute;
        /// <summary>
        /// ��ǰ���Ե� DefaultAttribute
        /// </summary>
        public DefaultAttribute DefaultAttribute {get{return _DefaultAttribute;} set{_DefaultAttribute=value;} }
        private List<ValidationAttribute> _ValidationAttributes;
        /// <summary>
        /// ��ǰ���Ե� ValidationAttribute ���б�
        /// </summary>
        public List<ValidationAttribute> ValidationAttributes {get{return _ValidationAttributes;} set{_ValidationAttributes=value;} }
        
		private IPropertyAccessor _PropertyAccessor;
		/// <summary>
        /// ��ǰ���Եĸ�ֵ/ȡֵ�������Ա��ⷴ��ĵ�Ч
        /// </summary>
		internal IPropertyAccessor PropertyAccessor {get{return _PropertyAccessor;} set{_PropertyAccessor=value;} }
        internal static EntityPropertyInfo Get( PropertyInfo property ) {
            EntityPropertyInfo ep = new EntityPropertyInfo();
            object[] arrAttr = property.GetCustomAttributes( typeof( ValidationAttribute ), true );
            foreach (Object at in arrAttr) {
                ep.ValidationAttributes.Add( at as ValidationAttribute );
            }
            ep.SaveAttribute = ReflectionUtil.GetAttribute( property, typeof( ColumnAttribute ) ) as ColumnAttribute;
            ep.LongTextAttribute = ReflectionUtil.GetAttribute( property, typeof( LongTextAttribute ) ) as LongTextAttribute;
            ep.DateTimeAttribute = ReflectionUtil.GetAttribute(property, typeof(DateTimeAttribute)) as DateTimeAttribute;
            ep.MoneyAttribute = ReflectionUtil.GetAttribute( property, typeof( MoneyAttribute ) ) as MoneyAttribute;
            ep.DecimalAttribute = ReflectionUtil.GetAttribute( property, typeof( DecimalAttribute ) ) as DecimalAttribute;
            ep.DefaultAttribute = ReflectionUtil.GetAttribute( property, typeof( DefaultAttribute ) ) as DefaultAttribute;
            ep.Property = property;
            ep.Name = property.Name;
            ep.Type = property.PropertyType;
            ep.SaveToDB = !property.IsDefined( typeof( NotSaveAttribute ), false );
            if (property.PropertyType is IList) {
                ep.IsList = true;
                ep.SaveToDB = false;
            }
            return ep;
        }
        /// <summary>
        /// ��ȡobj�ĵ�ǰ���Ե�ֵ
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Object GetValue( Object target ) {
            return PropertyAccessor.Get( target );
        }
        /// <summary>
        /// ��obj�ĵ�ǰ���Ը�ֵ
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue( Object target, Object value ) {
            PropertyAccessor.Set( target, value );
        }
        /// <summary>
        /// �Ƿ��ǳ��ı�
        /// </summary>
        public Boolean IsLongText {
            get {
                if (Type != typeof( String )) return false;
                return LongTextAttribute != null || ((SaveAttribute != null) && (SaveAttribute.Length > 255));
            }
        }
        /// <summary>
        /// ��ȡ���Ե�label(���ڱ���)
        /// </summary>
        public String Label {
            get {
                if (SaveAttribute == null) return Name;
                if (strUtil.IsNullOrEmpty( SaveAttribute.Label )) return Name;
                return SaveAttribute.Label;
            }
        }
    }
}