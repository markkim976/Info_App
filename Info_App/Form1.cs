using System; //C# 코드가 기본적으로 필요로 하는 클래스를 담고 있음
//namespace안에 있는 클래스를 사용하겠다고 컴파일러에게 알림
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //MySql Connection
//MySql.Data.dll 드라이버 (디렉토리 참조에서...) 설치 시 MySqlConnection, MySqlCommand, MySqlDataReader 등 이용 가능

namespace Info_App
{
    public partial class Form1 : Form
    {
        //SQL CONNECTION
        MySqlConnection conn = new MySqlConnection("Server=175.123.253.199; Database=test_schema;Uid=root;Pwd=eldkdlTlxl0101@@;");
        //conn이라는 변수를 만들어서 MySqlConnection 객체 생성
        public Form1()
        {
            InitializeComponent(); //디자인한 내용을 적용 시키기 위해 함수 호출
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Insert button
        {
            conn.Open(); //DB에 연결
            MySqlCommand cmd = new MySqlCommand("Insert into employee_info values(@Name, @Phone, @Address)", conn); //DB에 연결되는 동안 실행될 쿼리
            //MySqlCommand 클래스 사용하여 데이터베이스 operation 작업
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@Address", textBox3.Text);
            cmd.ExecuteNonQuery(); //명령을 수행하고 영향을 받은 행의 수를 반환하는 메서드 
            conn.Close(); //연결을 닫음
            MessageBox.Show("입력 되었습니다.");

        }

        //To display data
        public void display_data()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from employee_info", conn);
            MySqlDataAdapter data = new MySqlDataAdapter(cmd); //Query에 대한 결과 값을 가져옴, SQL commands와 데이터베이스 연결의 다리역할
            DataTable dt = new DataTable(); //DataTable 생성 
            data.Fill(dt); //테이블에 쿼리를 채움
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e) //Show Table button
        {
            display_data();

        }

        private void button2_Click(object sender, EventArgs e) //Delete button
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from employee_info where Name=@Name", conn); //Name 컬럼에서 데이터 삭제
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("삭제 되었습니다.");

        }

        private void button3_Click(object sender, EventArgs e) //Update button
        {

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("update employee_info set Name=@Name, Phone=@Phone, Address=@Address", conn);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@Address", textBox3.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("업데이트 되었습니다.");
        }

        private void button4_Click(object sender, EventArgs e) //Search button
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from  employee_info where Name=@Name or Phone=@Phone or Address=@Address", conn);
            //or을 써서 3개 중 하나만 입력 돼도 출력 가능
            cmd.Parameters.AddWithValue("@Name", textBox4.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox4.Text);
            cmd.Parameters.AddWithValue("@Address", textBox4.Text);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter data = new MySqlDataAdapter(cmd);
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
