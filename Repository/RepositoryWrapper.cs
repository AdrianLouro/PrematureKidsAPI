﻿using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private IParentRepository _parent;
        private IDoctorRepository _doctor;
        private IChildRepository _child;


        public IParentRepository Parent
        {
            get
            {
                if (_parent == null)
                {
                    _parent = new ParentRepository(_repoContext);
                }

                return _parent;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public IDoctorRepository Doctor
        {
            get
            {
                if (_doctor == null)
                {
                    _doctor = new DoctorRepository(_repoContext);
                }

                return _doctor;
            }
        }

        public IChildRepository Child
        {
            get
            {
                if (_child == null)
                {
                    _child = new ChildRepository(_repoContext);
                }

                return _child;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}