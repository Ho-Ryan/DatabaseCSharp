using MySqlConnector;
using System.Data;

namespace GetDataMysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnectionStringBuilder _builder = null;
        MySqlConnection _connection = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            // set these values correctly for your database server
            this._builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "super",
                Database = "instacfpt",
            };

            ConnectionState();
        }


        public async void ConnectionState()
        {
            _connection = new MySqlConnection(_builder.ConnectionString);
            await _connection.OpenAsync();
        }

        public async void getData(int postId)
        {
            await using var command = _connection.CreateCommand();
            command.CommandText = @"SELECT * FROM posts WHERE id = @postId;";
            command.Parameters.AddWithValue("@postId", postId);
            await using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var id = reader.GetInt32("id");
                label1.Text = $"{id}";
                var uniqid = reader.GetString("uniqid");
                label2.Text = uniqid;
                var title = reader.GetString("title");
                label3.Text = title;
                var image = reader.GetString("image");
                label4.Text = image;
                var date = reader.GetDateTime("date");
                label5.Text = date.ToString();

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Test test = new Test(Convert.ToInt32(label1.Text));
            test.NewId = 3;
            test.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var postId = (int)numericUpDown1.Value;
            getData(postId);
        }
    }
}
