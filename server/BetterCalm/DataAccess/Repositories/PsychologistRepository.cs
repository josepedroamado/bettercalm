using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PsychologistRepository : IPsychologistRepository
    {
        private DbContext context;
        private DbSet<Psychologist> psychologists;

        public PsychologistRepository(DbContext context)
        {
            this.context = context;
            this.psychologists = context.Set<Psychologist>();
        }

        public Psychologist Get(int id)
        {
            Psychologist psychologist = this.psychologists.Include(psychologist => psychologist.Illnesses).
                FirstOrDefault(psychologist => psychologist.Id == id);
            if (psychologist == null)
                throw new NotFoundException(id.ToString());
            return psychologist;
        }

        public void Add(Psychologist psychologist)
        {
            Psychologist psychologistToAdd = this.psychologists.FirstOrDefault(psycho => psycho.Id == psychologist.Id);
            if (psychologistToAdd != null)
            {
                throw new AlreadyExistsException(psychologistToAdd.Id.ToString());
            }
            else
            {
                this.psychologists.Add(psychologist);
                this.context.SaveChanges();
            }
        }
    }
}
