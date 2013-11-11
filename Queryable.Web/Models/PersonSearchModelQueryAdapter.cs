using System;
using System.Linq.Expressions;
using Queryable.Models;

namespace Queryable.Web.Models
{
    public class PersonSearchModelQueryAdapter : IQueryAdapter<Person>
    {
        private readonly PersonSearchModel _searchModel;

        private PersonSearchModelQueryAdapter()
        {
            _searchModel = new PersonSearchModel();
        }

        public PersonSearchModelQueryAdapter(PersonSearchModel searchModel)
        {
            _searchModel = searchModel;
        }

        public Expression<Func<Person, bool>> BuildQuery()
        {
            Expression<Func<Person, bool>> predicate = p => true;

            if (!String.IsNullOrEmpty(_searchModel.FirstName))
            {
                predicate = predicate.And(m => m.FirstName == _searchModel.FirstName);
            }

            if (!String.IsNullOrEmpty(_searchModel.LastName))
            {
                predicate = predicate.And(m => m.LastName == _searchModel.LastName);
            }

            if (_searchModel.DateOfBirth.HasValue)
            {
                predicate = predicate.And(m => m.DateOfBirth == _searchModel.DateOfBirth);
            }

            return predicate;
        }

        public static PersonSearchModelQueryAdapter AllPeopleSearchModel
        {
            get
            {
                return new PersonSearchModelQueryAdapter();
            }
        }
    }
}