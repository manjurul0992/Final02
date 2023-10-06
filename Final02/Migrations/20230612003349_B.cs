using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final02.Migrations
{
    public partial class B : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp1 = @"Create Procedure InsertCourse
            @CourseName varchar(55)
            As 
            Begin
            Insert into Courses (CourseName) Values(@CourseName)
            End";

            var sp2 = @"Create Procedure GetCourses
            As 
            Begin
            Select * from Courses
            End";

            var sp3 = @"Create Procedure UpdateCourse
            @CourseId int,
            @CourseName varchar(55)
            As 
            Begin
            Update  Courses Set CourseName=@CourseName where CourseId=@CourseId
            End";

            var sp4 = @"Create Procedure DeleteCourse
            @CourseId int            
            As 
            Begin
            Delete from Courses  where CourseId=@CourseId
            End";



            migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(sp3);
            migrationBuilder.Sql(sp4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp1 = @"DROP Procedure InsertCourse";
            migrationBuilder.Sql(sp1);

            var sp2 = @"Drop Procedure GetCourses";
            migrationBuilder.Sql(sp2);

            var sp3 = @"Drop Procedure UpdateCourse";
            migrationBuilder.Sql(sp3);

            var sp4 = @"Drop Procedure DeleteCourse";
            migrationBuilder.Sql(sp4);
        }
    }
}
