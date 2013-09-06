﻿using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Convencoes
{
    public class ClasseComumConvencao : IIdConvention, IClassConvention, IReferenceConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(("id_" + instance.EntityType.Name).ToLower());
            instance.GeneratedBy.Native();
        }

        public void Apply(IClassInstance instance)
        {
            instance.Table((instance.EntityType.Name +  "s").ToLower());
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(("id_" + instance.Property.PropertyType.Name).ToLower());

        }
    }
}