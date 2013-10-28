using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework.Entity
{
    /// <summary>
    /// Membuat Public Field dari Object bisa diakses pada Design Time
    /// </summary>
    [DebuggerNonUserCode]
    public sealed class EntityTDProvider : TypeDescriptionProvider
    {
        internal static TypeDescriptionProvider _baseProvider =
            TypeDescriptor.GetProvider(typeof(object));

        private static Dictionary<Type, FieldsToPropertiesTD> Cache =
            new Dictionary<Type, FieldsToPropertiesTD>();

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            if (BaseFramework.GetDefaultDp() == null)
                return new FieldsToPropertiesTD(_baseProvider
                   .GetTypeDescriptor(objectType), objectType);

            FieldsToPropertiesTD props;
            if (!Cache.TryGetValue(objectType, out props))
            {
                props = new FieldsToPropertiesTD(_baseProvider
                    .GetTypeDescriptor(objectType), objectType);
                Cache.Add(objectType, props);
            }
            return props;
        }

        [DebuggerNonUserCode]
        private class FieldPropertyDescriptor : PropertyDescriptor
        {
            private FieldInfo _field;

            public FieldPropertyDescriptor(FieldInfo field)
                : base(field.Name, (Attribute[])field.GetCustomAttributes(
                typeof(Attribute), true)) { _field = field; }
            public override bool Equals(object obj)
            {
                FieldPropertyDescriptor other = obj as FieldPropertyDescriptor;
                return other != null && other._field.Equals(_field);
            }
            public override int GetHashCode() { return _field.GetHashCode(); }
            public override bool IsReadOnly { get { return false; } }
            public override void ResetValue(object component) { }
            public override bool CanResetValue(object component) { return false; }
            public override bool ShouldSerializeValue(object component)
            { return true; }
            public override Type ComponentType
            { get { return _field.DeclaringType; } }
            public override Type PropertyType { get { return _field.FieldType; } }
            public override object GetValue(object component)
            { return _field.GetValue(component); }
            public override void SetValue(object component, object value)
            {
                _field.SetValue(component, value);
                OnValueChanged(component, EventArgs.Empty);
            }
        }

        [DebuggerNonUserCode]
        private class VirtualPropertyDescriptor : PropertyDescriptor
        {
            private PropertyInfo _pi;
            private string _PropName;

            public VirtualPropertyDescriptor(string PropName, PropertyInfo pi)
                : base(PropName, (Attribute[])pi.GetCustomAttributes(
                typeof(Attribute), true))
            {
                _pi = pi;
                _PropName = PropName;
            }
            public override bool Equals(object obj)
            {
                VirtualPropertyDescriptor other = obj as VirtualPropertyDescriptor;
                return other != null && other._pi.Equals(_pi);
            }
            public override int GetHashCode() { return _pi.GetHashCode(); }
            public override bool IsReadOnly { get { return false; } }
            public override void ResetValue(object component) { }
            public override bool CanResetValue(object component) { return false; }
            public override bool ShouldSerializeValue(object component)
            { return true; }
            public override Type ComponentType
            { get { return _pi.DeclaringType; } }
            public override Type PropertyType { get { return _pi.PropertyType; } }
            public override object GetValue(object component)
            { return _pi.GetValue(component, null); }
            public override void SetValue(object component, object value)
            {
                _pi.SetValue(component, value, null);
                OnValueChanged(component, EventArgs.Empty);
            }
        }
        
        //[DebuggerNonUserCode]
        private class FieldsToPropertiesTD : CustomTypeDescriptor
        {
            private Type _objectType;

            private static Dictionary<Type, PropertyDescriptorCollection> Cache =
                new Dictionary<Type, PropertyDescriptorCollection>();

            public FieldsToPropertiesTD(ICustomTypeDescriptor descriptor,
                Type objectType) : base(descriptor)
            {
                _objectType = objectType;
            }

            public override PropertyDescriptorCollection GetProperties()
            { return GetProperties(null); }
            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                if (BaseFramework.GetDefaultDp() == null)
                    return CreateProp();

                PropertyDescriptorCollection props;
                if (!Cache.TryGetValue(_objectType, out props))
                {
                    props = CreateProp();
                    Cache.Add(_objectType, props);
                }

                return props;
            }

            private PropertyDescriptorCollection CreateProp()
            {
                PropertyDescriptorCollection props =
                    new PropertyDescriptorCollection(null);

                foreach (PropertyDescriptor prop in base.GetProperties())
                    if (prop.IsBrowsable) props.Add(prop);

                foreach (FieldInfo field in _objectType.GetFields())
                    props.Add(new FieldPropertyDescriptor(field));

                EnableCancelEntityAttribute[] ddes = (EnableCancelEntityAttribute[])
                    _objectType.GetCustomAttributes(typeof(EnableCancelEntityAttribute), true);
                if (ddes.Length > 0)
                {
                    Type ice = _objectType.GetInterface("ICancelEntity");

                    string tmpFieldName = ddes[0].GetCancelDateTimeFieldName();
                    if(_objectType.GetMember(tmpFieldName, MemberTypes.Field | 
                        MemberTypes.Property, BindingFlags.NonPublic | BindingFlags.Public | 
                        BindingFlags.Instance).Length == 0)
                        props.Add(new VirtualPropertyDescriptor(tmpFieldName,
                            ice.GetProperty("CancelDateTime")));

                    tmpFieldName = ddes[0].GetCancelNotesFieldName();
                    if (_objectType.GetMember(tmpFieldName, MemberTypes.Field |
                        MemberTypes.Property, BindingFlags.NonPublic | BindingFlags.Public |
                        BindingFlags.Instance).Length == 0)
                        props.Add(new VirtualPropertyDescriptor(tmpFieldName,
                            ice.GetProperty("CancelNotes")));

                    tmpFieldName = ddes[0].GetCancelUserFieldName();
                    if (_objectType.GetMember(tmpFieldName, MemberTypes.Field |
                        MemberTypes.Property, BindingFlags.NonPublic | BindingFlags.Public |
                        BindingFlags.Instance).Length == 0)
                        props.Add(new VirtualPropertyDescriptor(tmpFieldName,
                            ice.GetProperty("CancelUser")));

                    tmpFieldName = ddes[0].GetCancelStatusFieldName();
                    if (_objectType.GetMember(tmpFieldName, MemberTypes.Field |
                        MemberTypes.Property, BindingFlags.NonPublic | BindingFlags.Public |
                        BindingFlags.Instance).Length == 0)
                        props.Add(new VirtualPropertyDescriptor(tmpFieldName,
                            ice.GetProperty("CancelStatus")));
                }

                return props;
            }
        }
    }
}
