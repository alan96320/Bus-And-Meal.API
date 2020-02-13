using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMeal.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrackedDate = table.Column<DateTime>(nullable: false),
                    TableName = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    RowId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusOrderVerificationHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(type: "varchar", maxLength: 10, nullable: true),
                    Orderdate = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusOrderVerificationHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusTime",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar", maxLength: 5, nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    DirectionEnum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowGrid = table.Column<int>(nullable: false),
                    LockedBusOrder = table.Column<DateTime>(nullable: false),
                    LockedMealOrder = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DormitoryBlock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DormitoryBlock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealOrderVerificationHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(type: "varchar", maxLength: 10, nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderedStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOrderVerificationHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealVendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    ContactName = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    ContactPhone = table.Column<string>(type: "varchar", maxLength: 15, nullable: true),
                    ContactEmail = table.Column<string>(type: "varchar", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealVendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleRights",
                columns: table => new
                {
                    MyProperty = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "varchar", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRights", x => x.MyProperty);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    GddbId = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    AdminStatus = table.Column<bool>(nullable: false),
                    LockTransStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HrCoreNo = table.Column<string>(type: "varchar", maxLength: 8, nullable: true),
                    Firstname = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    Lastname = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    Fullname = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusOrderEntryHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderEntryDate = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    DormitoryBlockId = table.Column<int>(nullable: true),
                    BusOrderVerificationHeaderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusOrderEntryHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusOrderEntryHeader_BusOrderVerificationHeader_BusOrderVerificationHeaderId",
                        column: x => x.BusOrderVerificationHeaderId,
                        principalTable: "BusOrderVerificationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusOrderEntryHeader_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusOrderEntryHeader_DormitoryBlock_DormitoryBlockId",
                        column: x => x.DormitoryBlockId,
                        principalTable: "DormitoryBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusOrderVerificationHeaderTotal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusOrderVerificationHeaderId = table.Column<int>(nullable: true),
                    BusTimeId = table.Column<int>(nullable: true),
                    DormitoryBlockId = table.Column<int>(nullable: true),
                    SumOrderQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusOrderVerificationHeaderTotal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusOrderVerificationHeaderTotal_BusOrderVerificationHeader_BusOrderVerificationHeaderId",
                        column: x => x.BusOrderVerificationHeaderId,
                        principalTable: "BusOrderVerificationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusOrderVerificationHeaderTotal_BusTime_BusTimeId",
                        column: x => x.BusTimeId,
                        principalTable: "BusTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusOrderVerificationHeaderTotal_DormitoryBlock_DormitoryBlockId",
                        column: x => x.DormitoryBlockId,
                        principalTable: "DormitoryBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealOrderEntryHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderEntryDate = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    MealOrderVerificationHeaderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOrderEntryHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOrderEntryHeader_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MealOrderEntryHeader_MealOrderVerificationHeader_MealOrderVerificationHeaderId",
                        column: x => x.MealOrderVerificationHeaderId,
                        principalTable: "MealOrderVerificationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    MealVendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealType_MealVendor_MealVendorId",
                        column: x => x.MealVendorId,
                        principalTable: "MealVendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDepartment_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDepartment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserModuleRights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleRightsMyProperty = table.Column<int>(nullable: true),
                    RightsId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModuleRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModuleRights_ModuleRights_ModuleRightsMyProperty",
                        column: x => x.ModuleRightsMyProperty,
                        principalTable: "ModuleRights",
                        principalColumn: "MyProperty",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserModuleRights_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusOrderEntryDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusOrderEntryHeaderId = table.Column<int>(nullable: false),
                    OrderQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusOrderEntryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusOrderEntryDetail_BusOrderEntryHeader_BusOrderEntryHeaderId",
                        column: x => x.BusOrderEntryHeaderId,
                        principalTable: "BusOrderEntryHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealOrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MealOrderEntryHeaderId = table.Column<int>(nullable: false),
                    MealTypeId = table.Column<int>(nullable: false),
                    OrderQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOrderDetail_MealOrderEntryHeader_MealOrderEntryHeaderId",
                        column: x => x.MealOrderEntryHeaderId,
                        principalTable: "MealOrderEntryHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealOrderDetail_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealOrderVerificationHeaderTotal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MealOrderVerificationHeaderId = table.Column<int>(nullable: false),
                    MealTypeId = table.Column<int>(nullable: false),
                    SumOrderQty = table.Column<int>(nullable: false),
                    AdjusmentQty = table.Column<int>(nullable: false),
                    SwipeQty = table.Column<int>(nullable: false),
                    LogBookQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOrderVerificationHeaderTotal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOrderVerificationHeaderTotal_MealOrderVerificationHeader_MealOrderVerificationHeaderId",
                        column: x => x.MealOrderVerificationHeaderId,
                        principalTable: "MealOrderVerificationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealOrderVerificationHeaderTotal_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderEntryDetail_BusOrderEntryHeaderId",
                table: "BusOrderEntryDetail",
                column: "BusOrderEntryHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderEntryHeader_BusOrderVerificationHeaderId",
                table: "BusOrderEntryHeader",
                column: "BusOrderVerificationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderEntryHeader_DepartmentId",
                table: "BusOrderEntryHeader",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderEntryHeader_DormitoryBlockId",
                table: "BusOrderEntryHeader",
                column: "DormitoryBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderVerificationHeaderTotal_BusOrderVerificationHeaderId",
                table: "BusOrderVerificationHeaderTotal",
                column: "BusOrderVerificationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderVerificationHeaderTotal_BusTimeId",
                table: "BusOrderVerificationHeaderTotal",
                column: "BusTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusOrderVerificationHeaderTotal_DormitoryBlockId",
                table: "BusOrderVerificationHeaderTotal",
                column: "DormitoryBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrderDetail_MealOrderEntryHeaderId",
                table: "MealOrderDetail",
                column: "MealOrderEntryHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrderDetail_MealTypeId",
                table: "MealOrderDetail",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrderEntryHeader_DepartmentId",
                table: "MealOrderEntryHeader",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrderEntryHeader_MealOrderVerificationHeaderId",
                table: "MealOrderEntryHeader",
                column: "MealOrderVerificationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrderVerificationHeaderTotal_MealOrderVerificationHeaderId",
                table: "MealOrderVerificationHeaderTotal",
                column: "MealOrderVerificationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOrderVerificationHeaderTotal_MealTypeId",
                table: "MealOrderVerificationHeaderTotal",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MealType_MealVendorId",
                table: "MealType",
                column: "MealVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_DepartmentId",
                table: "UserDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_UserId",
                table: "UserDepartment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleRights_ModuleRightsMyProperty",
                table: "UserModuleRights",
                column: "ModuleRightsMyProperty");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleRights_UserId",
                table: "UserModuleRights",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "BusOrderEntryDetail");

            migrationBuilder.DropTable(
                name: "BusOrderVerificationHeaderTotal");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Counter");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "MealOrderDetail");

            migrationBuilder.DropTable(
                name: "MealOrderVerificationHeaderTotal");

            migrationBuilder.DropTable(
                name: "UserDepartment");

            migrationBuilder.DropTable(
                name: "UserModuleRights");

            migrationBuilder.DropTable(
                name: "BusOrderEntryHeader");

            migrationBuilder.DropTable(
                name: "BusTime");

            migrationBuilder.DropTable(
                name: "MealOrderEntryHeader");

            migrationBuilder.DropTable(
                name: "MealType");

            migrationBuilder.DropTable(
                name: "ModuleRights");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BusOrderVerificationHeader");

            migrationBuilder.DropTable(
                name: "DormitoryBlock");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "MealOrderVerificationHeader");

            migrationBuilder.DropTable(
                name: "MealVendor");
        }
    }
}
