using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GamesSnake
{
    public partial class Form1 : Form
    {
       
        Graphics g;
        snake conRan;
        MaTran phamVi;
        ThucAn thucAn;
        delegate void ThongTin();
        
        ThongTin gameOver;
        ThongTin congDiem;
        ThongTin ketThuc;
        public Form1()
        {
            InitializeComponent();
            panel1.Width = 503;
            panel1.Height = 503;
            g = panel1.CreateGraphics();
            gameOver = GameOver;
            congDiem = CongDiem;
            ketThuc = KetThuc;
            reset();
        }

        private void GameOver()
        {
            g.DrawString("Bạn thua rồi!", new Font("Arial", 20), Brushes.Red, panel1.Width / 2 - 100, panel1.Height / 2);
            PauseButton.Enabled = false;
        }
        private void CongDiem()
        {
            DiemLabel.Text = (int.Parse(DiemLabel.Text) + 10).ToString();
            if ((int.Parse(DiemLabel.Text) % 50 == 0))
            {
                conRan.TangTocDo(30);
                LevelLabel.Text = (int.Parse(LevelLabel.Text) + 1).ToString();
            }
            //điểm cao nhất
            if ((int.Parse(DiemLabel.Text) > int.Parse(lbdiemcaonhat.Text)))
            {
                lbdiemcaonhat.Text = int.Parse(DiemLabel.Text).ToString();
            }
        }
        
      
        private void KetThuc()
        {
            g.DrawString("Giỏi rồi đó, Đừng chơi nữa (^_^)", new Font("Arial", 20), Brushes.Red, panel1.Width / 2 - 150, panel1.Height / 2);
        }
        void conRan_GoiSuLi(Point[] p, Point tailLast)
        {
            if (p.Length < 100)
            {
                phamVi.Change(p, 1);
                phamVi.Change(tailLast, 0);
                int temp = phamVi.Check(p[0]);
                if (temp == 1)
                {
                    conRan.Go(false);
                    this.Invoke(gameOver);
                    
                     // MessageBox.Show("games over !");
                  
                }
                else if (temp == 2)
                {
                    conRan.add();
                    phamVi.Change(thucAn.ToaDo, 0);
                    do
                    {
                        thucAn.TaoDiem();
                    } while (!phamVi.avail(thucAn.ToaDo));
                    thucAn.redraw();
                    phamVi.Change(thucAn.ToaDo, 2);
                    this.Invoke(congDiem);
                }
                thucAn.redraw();
            }
            else
            {
                conRan.Go(false);
                this.Invoke(ketThuc);
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            conRan.Go(true);
            PlayButton.Enabled = false;
            PauseButton.Enabled = true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            switch (keyData)
            {
                case Keys.Right:
                    conRan.diChuyen = DiChuyen.phai;
                    bHandled = true;
                    break;
                case Keys.Left:
                    conRan.diChuyen = DiChuyen.trai;
                    bHandled = true;
                    break;
                case Keys.Up:
                    conRan.diChuyen = DiChuyen.len;
                    bHandled = true;
                    break;
                case Keys.Down:
                    conRan.diChuyen = DiChuyen.xuong;
                    bHandled = true;
                    break;
            }
            return bHandled;
        }
        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (conRan != null)
                if (PauseButton.Text == "Tạm dừng")
                {
                    PauseButton.Text = "Tiếp tục";
                    conRan.Go(false);
                }
                else
                {
                    PauseButton.Text = "Tạm dừng";
                    conRan.Go(true);
                }
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            reset();
            PlayButton.Enabled = true;
            PauseButton.Enabled = false;
            PauseButton.Text = "Pause";

        }
        private void reset()
        {

            phamVi = new MaTran();
            thucAn = new ThucAn(g);
            if (conRan != null) conRan.Dispose();
            conRan = new snake(g, new Point(10, 5), Brushes.Red, Brushes.LightBlue, Pens.BlueViolet);
            conRan.GoiSuLi += new snake.thaydoi(conRan_GoiSuLi);
            do
            {
                thucAn.TaoDiem();
            } while (!phamVi.avail(thucAn.ToaDo));
            phamVi.Change(thucAn.ToaDo, 2);
            g.Clear(Color.White);
            DiemLabel.Text = "0";
            LevelLabel.Text = "1";
            conRan.ve();
            thucAn.redraw();

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            conRan.ve();
            thucAn.redraw();
        }

        private void LevelLabel_Click(object sender, EventArgs e)
        {

        }

        private void DiemLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Hdchoi_Click(object sender, EventArgs e)
        {
            {
                MessageBox.Show("Sử dụng 4 nút di chuyển trên bàn phím" + System.Environment.NewLine + "Đưa con rắn đến vị trí mồi xuất hiện để ăn" + System.Environment.NewLine + "Con rắn sẽ dài ra sau khi được ăn và tăng 10 điểm" + System.Environment.NewLine + "Số điểm chia hết cho 50 thì cấp độ được tăng lên đồng thời con rắn di chuyển nhanh hơn" + System.Environment.NewLine + "Bạn sẽ thua khi bạn đâm vào vật cản hoặc tự đâm vào chính mình!", "Hướng dẫn chơi");

            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
    }

