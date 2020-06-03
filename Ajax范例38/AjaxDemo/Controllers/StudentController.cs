using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxDal;
using AjaxBll;

namespace AjaxDemo.Controllers
{
    public class StudentController : Controller
    {
        StudentBll sb = new StudentBll();
        GradeBll gb = new GradeBll();
        //
        // GET: /Student/

        public ActionResult List()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult SelectStudent(int pageIndex=1)
        {
            int pageSize=10;
            int total=0;
            //调用方法，查询学生数据
            List<Student> list = sb.SelectPageList(pageSize, pageIndex, ref total);
            //使用匿名类型及数据投影解决json序列化循环引用的问题
            var query = from a in list
                        select new
                        {
                            a.StudentName,
                            a.StudentNo,
                            a.Phone,
                            a.LoginPwd

                        };
            //分页数据一起返回，返回的是单个实体，当中数据列表只是实体中的一个属性
            var resultJson = new
            {
                pageSize = pageSize,
                pageIndex = pageIndex,
                total = total,
                pageCount = total % pageSize == 0 ? total / pageSize : (total / pageSize + 1),
                query = query
            };
            return Json(resultJson,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除学生的方法
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public ActionResult DeleteStudent(string studentNo)
        {
            int count = sb.DeleteStudent(studentNo);
            return Content(count.ToString());
        }
        /// <summary>
        /// 添加学生的方法
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public ActionResult InsertStudent(Student stu)
        {
            int count = sb.InsertStudent(stu);
            return Content(count.ToString());
        }

        /// <summary>
        /// 修改学生的方法
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public ActionResult UpdateStudent(Student stu)
        {
            int count = sb.UpdateStudent(stu);
            return Content(count.ToString());
        }

        public ActionResult SelectOneStudent(string studentNo)
        {
            Student stu = new Student();
            stu = sb.SelectOneStudent(studentNo);
            //使用匿名类型解决json序列化过程造成的循环引用
            var query = new
            {
                stu.Sex,
                stu.GradeId,
                stu.LoginPwd,
                stu.Phone,
                stu.StudentName,
                stu.StudentNo,
                stu.Address,
                stu.Email,
                //将时间类型特殊处理，以字符串形式返回
                BornDate=stu.BornDate==null?""
                :Convert.ToDateTime(stu.BornDate).ToString("yyyy-MM-dd HH:mm:ss"),
            };
            return Json(query,JsonRequestBehavior.AllowGet);

        }
        public ActionResult SelectAllGrade()
        {
            List<Grade> list = gb.SelectAllGrade();
            var gList = from a in list
                        select new
                            {
                                a.GradeId,
                                a.GradeName
                            };
            return Json(gList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 验证手机号码是否重复
       /// </summary>
       /// <param name="phone">手机号码</param>
       /// <param name="studentNo">学号，若添加时验证请传空</param>
       /// <returns>结果大于0验证失败，手机号码重复</returns>
        public int validatePhone(string phone, string studentNo)
        {
            return sb.validatePhone(phone, studentNo);
        }
    }
}
