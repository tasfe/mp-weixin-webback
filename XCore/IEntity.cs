//------------------------------------------------------------------------------
//	�ļ����ƣ�System\IEntity.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace System {
    /// <summary>
    /// ���Ա� ORM �־û��Ķ��󣬶��Զ�ʵ���˱��ӿ�
    /// </summary>
    public interface IEntity {
        /// <summary>
        /// ÿһ���־û����󣬶�����һ�� Id ����
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// ��ȡ���Ե�ֵ(����ͨ�����䣬�ٶȽϿ�)
        /// </summary>
        /// <param name="propertyName">��������</param>
        /// <returns></returns>
        Object get( String propertyName );
        /// <summary>
        /// �������Ե�ֵ(����ͨ�����䣬�ٶȽϿ�)
        /// </summary>
        /// <param name="propertyName">��������</param>
        /// <param name="propertyValue">���Ե�ֵ</param>
        void set( String propertyName, Object propertyValue );
        /// <summary>
        /// ���������Ԫ���ݣ��Լ��ڶ����ѯ��ʱ����Ҫ�Ķ�����Ϣ��������
        /// </summary>
        //ObjectInfo state { get; set; }
    }
}