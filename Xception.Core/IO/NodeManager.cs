using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace Xception.Core.IO
{
    internal static class NodeManager
    {
        internal static W Combine<T, W>(this T val, Expression<Func<T, W>> expr) where W : class
        {
            var result = GetNode(val, expr);
            return (W)result;
        }

        internal static void AppendAttribute(this XmlNode node, string name, string value)
        {
            var doc = node.Combine(x => x.OwnerDocument);

            if (doc != null)
            {
                var att = doc.CreateAttribute(name);
                att.Value = value;
                node.Attributes.Append(att);
            }
            else
                throw new Exception("Invalid document provided.");
        }

        internal static object GetNode(object val, Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetNode(val, ((LambdaExpression)expr).Body);
                case ExpressionType.Parameter:
                    return val;
                case ExpressionType.MemberAccess:
                    {
                        var m = (MemberExpression)expr;
                        var inner = GetNode(val, m.Expression);
                        
                        if (m.Member is PropertyInfo)
                            return ((PropertyInfo)m.Member).GetGetMethod().Invoke(inner, null);
                        return ((FieldInfo)m.Member).GetValue(inner);
                    }
                default:
                    return null;
            }
        }
    }
}
