using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.Reflection;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal class MetadataBaseValuePropertyHandler : MemberTypePropertyHandler<MetadataBase>
    {
        private readonly PropertyInfo _propertyInfo;

        public MetadataBaseValuePropertyHandler(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public override object GetValue(MetadataBase baseObject)
        {
            var outputValue = base.GetValue(baseObject);

            switch (outputValue)
            {
                case Label l:
                    return l.LocalizedLabels.SingleOrDefault(i => i.LanguageCode == Session.Current.LanguageId)?.Label;
                case OptionSetMetadataBase osmb:
                    return osmb.Name;
                case BooleanManagedProperty bmp:
                    return bmp.Value;
                case AttributeRequiredLevelManagedProperty arlmp:
                    return arlmp.Value;
                case ConstantsBase<string> cbs:
                    return cbs.Value;
                default:
                    return outputValue;
            }
        }

        public override void SetValue(MetadataBase baseObject, object value)
        {
            Type propertyType = _propertyInfo.PropertyType;

            switch (propertyType)
            {
                case Type _ when propertyType == typeof(Label):
                    var l = (Label)base.GetValue(baseObject);
                    var labelValue = l.LocalizedLabels.SingleOrDefault(i => i.LanguageCode == Session.Current.LanguageId);

                    if (labelValue == null)
                    {
                        l.LocalizedLabels.Add(new LocalizedLabel((string)value, Session.Current.LanguageId));
                    }
                    else
                    {
                        labelValue.Label = (string)value;
                    }

                    break;
                case Type _ when propertyType == typeof(OptionSetMetadataBase):
                    var osmb = (OptionSetMetadataBase)base.GetValue(baseObject);
                    osmb.Name = (string)value;

                    break;
                case Type _ when propertyType == typeof(BooleanManagedProperty):
                    var bmp = (BooleanManagedProperty)base.GetValue(baseObject);
                    bmp.Value = (bool)value;

                    break;
                case Type _ when propertyType == typeof(AttributeRequiredLevelManagedProperty):
                    var arlmp = (AttributeRequiredLevelManagedProperty)base.GetValue(baseObject);
                    arlmp.Value = (AttributeRequiredLevel)value;

                    break;
                case Type _ when propertyType == typeof(ConstantsBase<string>):
                    var cbs = (ConstantsBase<string>)base.GetValue(baseObject);
                    cbs.Value = (string)value;

                    break;
                default:
                    base.SetValue(baseObject, value);
                    break;
            }
        }
    }
}
