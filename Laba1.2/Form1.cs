using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1._2
{
    public partial class Form1 : Form
    {
        private const string ImagesDirectory = @"C:\Users\Виталя\Desktop\Laba1.2-master\Pictyre"; 
        private string currentImagePath; 

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string query = textBox2.Text;
            if (!string.IsNullOrWhiteSpace(query))
            {
                string imageUrl = await GetRandomImageUrl(query);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    currentImagePath = imageUrl;
                    pictureBox2.Load(imageUrl);
                }
                else
                {
                    MessageBox.Show("Изображение не найдено.");
                }
            }
        }

        private async Task<string> GetRandomImageUrl(string query)
        {
            try
            {
                var searchPattern = $"{query}*.jpg"; 
                var files = Directory.GetFiles(ImagesDirectory, searchPattern);

                if (files.Length > 0)
                {
                    Random rand = new Random();
                    int randomIndex = rand.Next(files.Length);
                    return files[randomIndex]; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                saveFileDialog.FileName = Path.GetFileName(currentImagePath);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(currentImagePath, saveFileDialog.FileName, true);
                        MessageBox.Show("Изображение успешно скачано!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при скачивании изображения: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите изображение для скачивания.");
            }
        }
    }
}
