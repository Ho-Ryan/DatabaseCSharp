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
                Database = "Eleves",
            };

            ConnectionState();
        }


        public async void ConnectionState()
        {
            _connection = new MySqlConnection(_builder.ConnectionString);
            await _connection.OpenAsync();
        }

        public async void GetData(int postId)
        {
            await using var command = _connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Eleves WHERE id = @postId;";
            command.Parameters.AddWithValue("@postId", postId);
            await using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var id = reader.GetInt32("id");
                label1.Text = $"{id}";
                var nom = reader.GetString("nom");
                label2.Text = nom;
                var prenom = reader.GetString("prenom");
                label3.Text = prenom;
                textBox1.Text = nom;
                textBox2.Text = prenom;
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
            GetData(postId);
        }

        public async void SetData()
        {
            await using var command = _connection.CreateCommand();
            command.CommandText = @"INSERT INTO Eleves(nom, prenom) VALUE(@nom, @prenom);";
            command.Parameters.AddWithValue("@nom", textBox1.Text);
            command.Parameters.AddWithValue("@prenom", textBox2.Text);
            await using var execute = await command.ExecuteReaderAsync();
            execute.Read();
            MessageBox.Show("Votre valeur a été ajouté");
        }

        public async void ModifyData(int postId)
        {
            await using var command = _connection.CreateCommand();
            command.CommandText = @"UPDATE Eleves SET nom = @nom WHERE id = @postId; UPDATE Eleves SET prenom = @prenom WHERE id = @postId; SELECT * FROM Eleves WHERE id = @postId;";
            command.Parameters.AddWithValue("@nom", textBox1.Text);
            command.Parameters.AddWithValue("@prenom", textBox2.Text);
            command.Parameters.AddWithValue("@postId", postId);

            await using var execute = await command.ExecuteReaderAsync();
            //execute.Read();
            while (execute.Read())
            {
                var id = execute.GetInt32("id");
                label1.Text = $"{id}";
                var nom = execute.GetString("nom");
                label2.Text = nom;
                var prenom = execute.GetString("prenom");
                label3.Text = prenom;
            }
            MessageBox.Show("Votre valeur a été modifié");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = (int)numericUpDown1.Value;
            ModifyData(id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            label1.Text = "ID";
            label2.Text = "Nom";
            label3.Text = "Prénom";
            numericUpDown1.Value = 0;
        }
    }
}
