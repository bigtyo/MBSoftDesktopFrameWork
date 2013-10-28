using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace Template
{
    public class Entity1 : ParentEntity
    {
        public EntityCollection<ChildEntity1> DetilEntity;
    }

    public class ChildEntity1 : ChildEntity<Entity1> 
    {

    }
}
