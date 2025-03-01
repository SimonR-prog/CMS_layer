using Data.Entities;
using System.ComponentModel;

namespace Tests.TestData;

public static class Test_Data
{   
    /* Lists of customer and project entities which will be added/not added to database to test against.
     * Invalid testdata contains duplicate email, invalid length of customer name, duplicate Id on customer, 
     * null projectname, no customer at all and customer id which doesn't exist.
     */


    public static readonly CustomerEntity[] CustomerEntity_Valid_TestData =
        [
            new CustomerEntity { Id = 1, CustomerName = "Nackademin", Email = "Nackademin@email.com"},
            new CustomerEntity { Id = 2, CustomerName = "SWE AB", Email = "SWEab@email.com"},
            new CustomerEntity { Id = 3, CustomerName = "Faber-Castell", Email = "Faber@email.com"}
        ];

    public static readonly CustomerEntity[] CustomerEntity_Invalid_TestData =
        [
            new CustomerEntity { Id = 4, CustomerName = "Same Email As Nackademin", Email = "Nackademin@email.com"},
            new CustomerEntity { Id = 1, CustomerName = "Same Id As Nackademin", Email = "NackaTwo@email.com"},
            new CustomerEntity { Id = 5, CustomerName = new string('A', 255), Email = "Long@name.com"}
        ];

    public static readonly ProjectEntity[] ProjectEntity_Valid_TestData =
        [
            new ProjectEntity { Id = 1, ProjectName = "Test project one", Description = "", CustomerId = 2 },
            new ProjectEntity { Id = 2, ProjectName = "Test project two", Description = "Test description one.", CustomerId = 3 },
            new ProjectEntity { Id = 3, ProjectName = "Test project three", Description = "Test description two.", CustomerId = 2 },
            new ProjectEntity { Id = 4, ProjectName = "Test null description", Description = null, CustomerId = 1}
        ];

    public static readonly ProjectEntity[] ProjectEntity_Invalid_TestData =
        [
            new ProjectEntity { Id = 5, ProjectName = null!, Description = "Test of null project name.", CustomerId = 1 },
            new ProjectEntity { Id = 6, ProjectName = "NoCustomer", Description = "Test no customer." },
            new ProjectEntity { Id = 7, ProjectName = "Invalid customer id", Description = "Test of customer nonexistent", CustomerId = 999}
        ];
}
