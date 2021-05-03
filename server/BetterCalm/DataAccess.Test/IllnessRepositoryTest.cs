using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess.Test
{
    [TestClass]
    public class IllnessRepositoryTest
    {
        private DbContext context;
        private DbConnection connection;

        public IllnessRepositoryTest()
        {
            this.connection = new SqliteConnection("Filename=:memory:");
            this.context = new BetterCalmContext(
                new DbContextOptionsBuilder<BetterCalmContext>()
                .UseSqlite(connection)
                .Options);
        }

        [TestInitialize]
        public void Setup()
        {
            this.connection.Open();
            this.context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOk()
        {
            Illness expectedIllness = new Illness { Id = 1, Name = "Stress" };
            this.context.Add(expectedIllness);
            this.context.SaveChanges();

            IllnessRepository repository = new IllnessRepository(this.context);

            Illness obtainedIllness = repository.Get(expectedIllness.Id);

            Assert.AreEqual(expectedIllness, obtainedIllness);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetNotFound()
        {
            int expectedIllnessId = 1;

            IllnessRepository repository = new IllnessRepository(this.context);

            Illness obtainedIllness = repository.Get(expectedIllnessId);

            Assert.IsNull(obtainedIllness);
        }

        [TestMethod]
        public void GetAllOk()
        {
            List<Illness> expectedIllnesses = GetAllOkExpected();
            foreach (Illness illness in expectedIllnesses)
            {
                this.context.Add(illness);
            }
            this.context.SaveChanges();

            IllnessRepository repository = new IllnessRepository(this.context);

            IEnumerable<Illness> obtainedIllnesses = repository.GetAll();

            Assert.IsTrue(expectedIllnesses.SequenceEqual(obtainedIllnesses));
        }

        private List<Illness> GetAllOkExpected()
        {
            Illness depression = new Illness()
            {
                Id = 1,
                Name = "Depression"
            };

            Illness stress = new Illness()
            {
                Id = 2,
                Name = "Stress"
            };
            return new List<Illness>() { depression, stress };         
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetAllWithoutIllnesses()
        {
            IllnessRepository repository = new IllnessRepository(this.context);
            IEnumerable<Illness> obtainedIllnesses = repository.GetAll();
            Assert.IsNull(obtainedIllnesses);
        }
    }
}  
