using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IPsychologistRepository
	{
		void Add(Psychologist psychologist);
		Psychologist Get(int id);
		IEnumerable<Psychologist> GetAll();
		void Update(Psychologist psychologist);
	}
}
