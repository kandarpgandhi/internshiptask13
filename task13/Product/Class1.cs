using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Class1
    {
        public void InsertDataIntoPurchase_Table(int pid,string pno,DateTime d,int t)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GI21EV1\SQLEXPRESS; Initial Catalog=Item; Integrated Security=true;");
            con.Open();
            SqlCommand pNo = new SqlCommand("spgetLastProductNo",con);
            string number = (string)pNo.ExecuteScalar();
            int temp = 0;
            string temp1 = "";
            if (number == null)
            {
                temp1 = "0001";
            }
            else
            {
                if (int.Parse(number) < 10)
                {
                    temp = int.Parse(number) + 1;
                    temp1 = "000" + temp.ToString();
                }
                else if (int.Parse(number) > 9 && int.Parse(number) < 100)
                {
                    temp = int.Parse(number) + 1;
                    temp1 = "00" + temp.ToString();
                }
                else if (int.Parse(number) > 99 && int.Parse(number) < 1000)
                {
                    temp = int.Parse(number) + 1;
                    temp1 = "0" + temp.ToString();
                }
                else
                {
                    temp = int.Parse(number) + 1;
                    temp1 = temp.ToString();
                }
            }
            string q1 = "insert into Purchase_Table (Purchase_Id,Purchase_No,Date,Total) values('"+pid+ "','"+temp1+ "','"+d+ "','"+t+"')";
            SqlCommand cmd = new SqlCommand(q1, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.ExecuteNonQuery();
            con.Close();
        }
        public void InsertDataIntoPurchase_Product(int pid, string name, int q, int a)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GI21EV1\SQLEXPRESS; Initial Catalog=Item; Integrated Security=true;");
            con.Open();
            string q1 = "insert into Purchase_Product (Purchase_Id,Item,Quantity,Amount) values('" + pid + "','" + name + "','" + q + "','" + a + "')";
            SqlCommand cmd = new SqlCommand(q1, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateDataIntoPurchase_Table(int pid,int id,int amount)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GI21EV1\SQLEXPRESS; Initial Catalog=Item; Integrated Security=true;");
            con.Open();
            string q= "select Total from Purchase_Table where Purchase_Id='" + id + "'";
            SqlCommand cmdq = new SqlCommand(q, con);
            int oldTotal=(int)cmdq.ExecuteScalar();
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmdq);
            //int oldTotal = sqlDataAdapter.SelectCommand.ExecuteNonQuery();
            

            string q2 = "select Amount from Purchase_Product where PurchaseProductId='" + pid + "'";
            SqlCommand newt = new SqlCommand(q2, con);
            //SqlDataAdapter sqlDataAdaptert = new SqlDataAdapter(newt);
            int a = (int)newt.ExecuteScalar();//sqlDataAdapter.SelectCommand.ExecuteNonQuery();

            int newTotal = oldTotal - a +amount;

            string q1 = "update Purchase_Table set Total='" + newTotal + "' where Purchase_Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q1, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateDataIntoPurchase_Product(int pid,int q,int a)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GI21EV1\SQLEXPRESS; Initial Catalog=Item; Integrated Security=true;");
            con.Open();
            string q1 = "update Purchase_Product set Quantity='"+q+"',Amount='"+a+"' where PurchaseProductId='"+pid+"'";
            SqlCommand cmd = new SqlCommand(q1, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.ExecuteNonQuery();
            con.Close();
        }
    }
}
