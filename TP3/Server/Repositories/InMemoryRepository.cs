using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using IFT585_TP3.Common;

namespace IFT585_TP3.Server.Repositories
{
    public class InMemoryRepository<EntityType> where EntityType : class, IDeepClonable<EntityType>
    {
        protected Dictionary<object, EntityType> entities = new Dictionary<object, EntityType>();

        private MemberInfo idMember;

        public InMemoryRepository()
        {
            MemberInfo[] members = typeof(EntityType).GetMembers();
            foreach (MemberInfo member in members)
            {
                if (member.GetCustomAttribute(typeof(Id)) != null)
                {
                    idMember = member;
                    break;
                }
            }
        }

        public void Create(EntityType toCreate)
        {
            if (idMember.MemberType == MemberTypes.Field)
            {
                entities.Add(((FieldInfo)idMember).GetValue(toCreate), toCreate.DeepClone());
            }
            else
            {
                entities.Add(((PropertyInfo)idMember).GetValue(toCreate), toCreate.DeepClone());
            }
        }

        public EntityType Retrieve(object id)
        {
            return entities.ContainsKey(id) ? entities[id].DeepClone() : null;
        }

        public IEnumerable<EntityType> RetrieveAll()
        {
            return entities.Values.ToList().ConvertAll(entity => entity.DeepClone());
        }

        public void Update(EntityType toUpdate)
        {
            object id = idMember.MemberType == MemberTypes.Field ? ((FieldInfo)idMember).GetValue(toUpdate) : ((PropertyInfo)idMember).GetValue(toUpdate);
            if (!entities.ContainsKey(id))
            {
                throw new System.Exception("Tried to update a non-existing entity.");
            }

            entities[id] = toUpdate.DeepClone();
        }

        public void Delete(object id)
        {
            if (!entities.Remove(id))
            {
                throw new System.Exception("Tried to delete a non-existing entity.");
            }
        }

        public bool Exists(object id)
        {
            return entities.ContainsKey(id);
        }
    }
}
