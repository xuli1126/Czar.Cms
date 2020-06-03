using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjaxDal
{
    public class StudentDal
    {
        /// <summary>
        /// 分页查询学生表数据
        /// </summary>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">总数据条数</param>
        /// <returns></returns>
        public List<Student> SelectPageList(int pageSize, int pageIndex, ref int total)
        {
            List<Student> list = new List<Student>();
            SchoolEntities entity = new SchoolEntities();
            total = entity.Student.Count();
            list = (from a in entity.Student
                    orderby a.StudentNo
                    select a).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return list;
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public int DeleteStudent(string studentNo)
        {
            int count = 0;
            SchoolEntities entity = new SchoolEntities();
            Student deleteStudent=(from a in entity.Student 
                                   where a.StudentNo==studentNo 
                                   select a).FirstOrDefault();
           //判断该学生是否有关联的成绩数据
            if (deleteStudent.Result.Count() > 0)
            {
                count = -1;
            }
            else
            {
                entity.Student.Remove(deleteStudent);
                count = entity.SaveChanges();
            }
            return count;
        }
        /// <summary>
        /// 根据学号查询1个学生的信息的方法
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public Student SelectOneStudent(string studentNo)
        {
            SchoolEntities entity = new SchoolEntities();
            Student stu = new Student();
            stu = entity.Student.Where(m => m.StudentNo == studentNo)
                .FirstOrDefault();
            return stu;
        }
        /// <summary>
        /// 添加学生的方法
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int InsertStudent(Student stu)
        {
            SchoolEntities entity = new SchoolEntities();
            entity.Student.Add(stu);
            int count = entity.SaveChanges();
            return count;
        }
        /// <summary>
        /// 修改学生的方法
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int UpdateStudent(Student stu)
        {
            SchoolEntities entity = new SchoolEntities();
            Student upStu = (from a in entity.Student
                             where a.StudentNo == stu.StudentNo
                             select a).FirstOrDefault();
            int count = 0;
            if (upStu != null)
            {
                upStu.StudentName = stu.StudentName;
                upStu.Address = stu.Address;
                upStu.BornDate = stu.BornDate;
                upStu.Email = stu.Email;
                upStu.GradeId = stu.GradeId;
                upStu.LoginPwd = stu.LoginPwd;
                upStu.Phone = stu.Phone;
                upStu.Sex = stu.Sex;
                count = entity.SaveChanges();
            }
            return count;
        }
       /// <summary>
        /// 验证手机号码是否重复
       /// </summary>
       /// <param name="phone">手机号码</param>
       /// <param name="studentNo">学号，若添加时验证请传空</param>
       /// <returns>结果大于0验证失败，手机号码重复</returns>
        public int validatePhone(string phone,string studentNo)
        {
            SchoolEntities entity = new SchoolEntities();
            int count = 0;
            if (string.IsNullOrEmpty(studentNo))
            {//添加时验证
                count = entity.Student.Where(m => m.Phone == phone).Count();
            }
            else
            {//修改时验证
                count = entity.Student.Where(m => m.Phone == phone
                    &&m.StudentNo!=studentNo).Count();
            }
            return count;
        }
    }
}
