using System;
using System.Collections.Generic;
using System.Text;
using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Tests
{
    public class MechanicTests
    {
        [Fact]
        public void Mechanic_Should_Have_AssignedJobs_List()
        {
            // Arrange
            var mechanic = new Mechanic();
            // Act
            var assignedJobs = mechanic.AssignedJobs;
            mechanic.AssignedJobs = new List<Job>
            {
                new Job { Id = Guid.NewGuid(), Description = "Fix engine" },
                new Job { Id = Guid.NewGuid(), Description = "Change oil" }
            };
            // Assert
            Assert.NotNull(assignedJobs);
            Assert.IsType<List<Job>>(assignedJobs);
            Assert.Equal(2, mechanic.AssignedJobs.Count);
        }
        [Fact]
        public void Mechanic_ShouldHave_Unique_Id()
        {
            // Arrange
            var mechanic1 = new Mechanic();
            var mechanic2 = new Mechanic();
            // Act
            var id1 = mechanic1.Id;
            var id2 = mechanic2.Id;
            // Assert
            Assert.NotEqual(id1, id2);
        }
        [Fact]
        public void Mechanic_Should_Derive_From_Person()
        {
            // Arrange
            var mechanic = new Mechanic();
            // Act
            var isPerson = mechanic is Person;
            // Assert
            Assert.True(isPerson);
        }
    }
}
