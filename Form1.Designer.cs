namespace apListaLigada
{
  partial class FrmAlunos
  {
    /// <summary>
    /// Variável de designer necessária.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Limpar os recursos que estão sendo usados.
    /// </summary>
    /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Código gerado pelo Windows Form Designer

    /// <summary>
    /// Método necessário para suporte ao Designer - não modifique 
    /// o conteúdo deste método com o editor de código.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlunos));
            this.label2 = new System.Windows.Forms.Label();
            this.txtRA = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.dlgAbrir = new System.Windows.Forms.OpenFileDialog();
            this.dlgSalvar = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnInicio = new System.Windows.Forms.ToolStripButton();
            this.btnAnterior = new System.Windows.Forms.ToolStripButton();
            this.btnProximo = new System.Windows.Forms.ToolStripButton();
            this.btnFim = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCadastro = new System.Windows.Forms.TabPage();
            this.tableData = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Palavra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tpForca = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.btnK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnR = new System.Windows.Forms.Button();
            this.btnS = new System.Windows.Forms.Button();
            this.btnQ = new System.Windows.Forms.Button();
            this.btnT = new System.Windows.Forms.Button();
            this.btnP = new System.Windows.Forms.Button();
            this.btnO = new System.Windows.Forms.Button();
            this.btnN = new System.Windows.Forms.Button();
            this.btnL = new System.Windows.Forms.Button();
            this.btnJ = new System.Windows.Forms.Button();
            this.btnI = new System.Windows.Forms.Button();
            this.btnH = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.btnF = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.btnM = new System.Windows.Forms.Button();
            this.btnU = new System.Windows.Forms.Button();
            this.btnV = new System.Windows.Forms.Button();
            this.btnW = new System.Windows.Forms.Button();
            this.btnY = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableData)).BeginInit();
            this.tpForca.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dica:";
            // 
            // txtRA
            // 
            this.txtRA.Location = new System.Drawing.Point(79, 18);
            this.txtRA.MaxLength = 30;
            this.txtRA.Name = "txtRA";
            this.txtRA.Size = new System.Drawing.Size(207, 24);
            this.txtRA.TabIndex = 3;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(79, 48);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(524, 24);
            this.txtNome.TabIndex = 4;
            // 
            // dlgAbrir
            // 
            this.dlgAbrir.DefaultExt = "*.txt";
            this.dlgAbrir.Filter = "Arquivos de texto|*.txt|Qualquer arquivo|*.*";
            // 
            // dlgSalvar
            // 
            this.dlgSalvar.DefaultExt = "*.txt";
            this.dlgSalvar.Title = "Selecione o arquivo para gravação dos dados";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInicio,
            this.btnAnterior,
            this.btnProximo,
            this.btnFim,
            this.toolStripSeparator1,
            this.btnBuscar,
            this.toolStripSeparator3,
            this.btnNovo,
            this.btnEditar,
            this.btnCancelar,
            this.btnSalvar,
            this.btnExcluir,
            this.toolStripSeparator2,
            this.btnSair});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(634, 38);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnInicio
            // 
            this.btnInicio.Image = ((System.Drawing.Image)(resources.GetObject("btnInicio.Image")));
            this.btnInicio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(40, 35);
            this.btnInicio.Text = "Início";
            this.btnInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Image = ((System.Drawing.Image)(resources.GetObject("btnAnterior.Image")));
            this.btnAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(54, 35);
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnProximo
            // 
            this.btnProximo.Image = ((System.Drawing.Image)(resources.GetObject("btnProximo.Image")));
            this.btnProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(55, 35);
            this.btnProximo.Text = "Próximo";
            this.btnProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // btnFim
            // 
            this.btnFim.Image = ((System.Drawing.Image)(resources.GetObject("btnFim.Image")));
            this.btnFim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFim.Name = "btnFim";
            this.btnFim.Size = new System.Drawing.Size(36, 35);
            this.btnFim.Text = "Final";
            this.btnFim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFim.Click += new System.EventHandler(this.btnFim_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // btnBuscar
            // 
            this.btnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(23, 35);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // btnNovo
            // 
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(40, 35);
            this.btnNovo.Text = "Novo";
            this.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNovo.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(41, 35);
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(57, 35);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(45, 35);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(30, 35);
            this.btnSair.Text = "Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpCadastro);
            this.tabControl1.Controls.Add(this.tpForca);
            this.tabControl1.Location = new System.Drawing.Point(12, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 326);
            this.tabControl1.TabIndex = 22;
            this.tabControl1.Enter += new System.EventHandler(this.tabControl1_Enter);
            // 
            // tpCadastro
            // 
            this.tpCadastro.Controls.Add(this.tableData);
            this.tpCadastro.Controls.Add(this.label1);
            this.tpCadastro.Controls.Add(this.label2);
            this.tpCadastro.Controls.Add(this.txtRA);
            this.tpCadastro.Controls.Add(this.txtNome);
            this.tpCadastro.Location = new System.Drawing.Point(4, 26);
            this.tpCadastro.Name = "tpCadastro";
            this.tpCadastro.Padding = new System.Windows.Forms.Padding(3);
            this.tpCadastro.Size = new System.Drawing.Size(609, 296);
            this.tpCadastro.TabIndex = 0;
            this.tpCadastro.Text = "Cadastro";
            this.tpCadastro.UseVisualStyleBackColor = true;
            // 
            // tableData
            // 
            this.tableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Palavra,
            this.Dica});
            this.tableData.Location = new System.Drawing.Point(15, 78);
            this.tableData.Name = "tableData";
            this.tableData.Size = new System.Drawing.Size(588, 193);
            this.tableData.TabIndex = 6;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Palavra
            // 
            this.Palavra.Frozen = true;
            this.Palavra.HeaderText = "Palavra";
            this.Palavra.Name = "Palavra";
            // 
            // Dica
            // 
            this.Dica.HeaderText = "Dica";
            this.Dica.Name = "Dica";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Palavra:";
            // 
            // tpForca
            // 
            this.tpForca.Controls.Add(this.btnY);
            this.tpForca.Controls.Add(this.btnW);
            this.tpForca.Controls.Add(this.btnV);
            this.tpForca.Controls.Add(this.btnU);
            this.tpForca.Controls.Add(this.btnM);
            this.tpForca.Controls.Add(this.button2);
            this.tpForca.Controls.Add(this.btnX);
            this.tpForca.Controls.Add(this.btnK);
            this.tpForca.Controls.Add(this.button1);
            this.tpForca.Controls.Add(this.btnR);
            this.tpForca.Controls.Add(this.btnS);
            this.tpForca.Controls.Add(this.btnQ);
            this.tpForca.Controls.Add(this.btnT);
            this.tpForca.Controls.Add(this.btnP);
            this.tpForca.Controls.Add(this.btnO);
            this.tpForca.Controls.Add(this.btnN);
            this.tpForca.Controls.Add(this.btnL);
            this.tpForca.Controls.Add(this.btnJ);
            this.tpForca.Controls.Add(this.btnI);
            this.tpForca.Controls.Add(this.btnH);
            this.tpForca.Controls.Add(this.btnG);
            this.tpForca.Controls.Add(this.btnF);
            this.tpForca.Controls.Add(this.btnE);
            this.tpForca.Controls.Add(this.btnD);
            this.tpForca.Controls.Add(this.btnC);
            this.tpForca.Controls.Add(this.btnB);
            this.tpForca.Controls.Add(this.btnA);
            this.tpForca.Controls.Add(this.listView1);
            this.tpForca.Location = new System.Drawing.Point(4, 26);
            this.tpForca.Name = "tpForca";
            this.tpForca.Padding = new System.Windows.Forms.Padding(3);
            this.tpForca.Size = new System.Drawing.Size(609, 296);
            this.tpForca.TabIndex = 2;
            this.tpForca.Text = "Forca";
            this.tpForca.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(525, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnX
            // 
            this.btnX.Location = new System.Drawing.Point(277, 133);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(25, 23);
            this.btnX.TabIndex = 21;
            this.btnX.Text = "X";
            this.btnX.UseVisualStyleBackColor = true;
            // 
            // btnK
            // 
            this.btnK.Location = new System.Drawing.Point(525, 75);
            this.btnK.Name = "btnK";
            this.btnK.Size = new System.Drawing.Size(25, 23);
            this.btnK.TabIndex = 20;
            this.btnK.Text = "K";
            this.btnK.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Z";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnR
            // 
            this.btnR.Location = new System.Drawing.Point(401, 104);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(25, 23);
            this.btnR.TabIndex = 18;
            this.btnR.Text = "R";
            this.btnR.UseVisualStyleBackColor = true;
            // 
            // btnS
            // 
            this.btnS.Location = new System.Drawing.Point(432, 104);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(25, 23);
            this.btnS.TabIndex = 17;
            this.btnS.Text = "S";
            this.btnS.UseVisualStyleBackColor = true;
            // 
            // btnQ
            // 
            this.btnQ.Location = new System.Drawing.Point(370, 104);
            this.btnQ.Name = "btnQ";
            this.btnQ.Size = new System.Drawing.Size(25, 23);
            this.btnQ.TabIndex = 16;
            this.btnQ.Text = "Q";
            this.btnQ.UseVisualStyleBackColor = true;
            // 
            // btnT
            // 
            this.btnT.Location = new System.Drawing.Point(463, 104);
            this.btnT.Name = "btnT";
            this.btnT.Size = new System.Drawing.Size(25, 23);
            this.btnT.TabIndex = 15;
            this.btnT.Text = "T";
            this.btnT.UseVisualStyleBackColor = true;
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(339, 104);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(25, 23);
            this.btnP.TabIndex = 14;
            this.btnP.Text = "P";
            this.btnP.UseVisualStyleBackColor = true;
            // 
            // btnO
            // 
            this.btnO.Location = new System.Drawing.Point(308, 104);
            this.btnO.Name = "btnO";
            this.btnO.Size = new System.Drawing.Size(25, 23);
            this.btnO.TabIndex = 13;
            this.btnO.Text = "O";
            this.btnO.UseVisualStyleBackColor = true;
            // 
            // btnN
            // 
            this.btnN.Location = new System.Drawing.Point(277, 104);
            this.btnN.Name = "btnN";
            this.btnN.Size = new System.Drawing.Size(25, 23);
            this.btnN.TabIndex = 12;
            this.btnN.Text = "N";
            this.btnN.UseVisualStyleBackColor = true;
            // 
            // btnL
            // 
            this.btnL.Location = new System.Drawing.Point(215, 104);
            this.btnL.Name = "btnL";
            this.btnL.Size = new System.Drawing.Size(25, 23);
            this.btnL.TabIndex = 11;
            this.btnL.Text = "L";
            this.btnL.UseVisualStyleBackColor = true;
            // 
            // btnJ
            // 
            this.btnJ.Location = new System.Drawing.Point(494, 75);
            this.btnJ.Name = "btnJ";
            this.btnJ.Size = new System.Drawing.Size(25, 23);
            this.btnJ.TabIndex = 10;
            this.btnJ.Text = "J";
            this.btnJ.UseVisualStyleBackColor = true;
            // 
            // btnI
            // 
            this.btnI.Location = new System.Drawing.Point(463, 75);
            this.btnI.Name = "btnI";
            this.btnI.Size = new System.Drawing.Size(25, 23);
            this.btnI.TabIndex = 9;
            this.btnI.Text = "I";
            this.btnI.UseVisualStyleBackColor = true;
            // 
            // btnH
            // 
            this.btnH.Location = new System.Drawing.Point(432, 75);
            this.btnH.Name = "btnH";
            this.btnH.Size = new System.Drawing.Size(25, 23);
            this.btnH.TabIndex = 8;
            this.btnH.Text = "H";
            this.btnH.UseVisualStyleBackColor = true;
            // 
            // btnG
            // 
            this.btnG.Location = new System.Drawing.Point(401, 75);
            this.btnG.Name = "btnG";
            this.btnG.Size = new System.Drawing.Size(25, 23);
            this.btnG.TabIndex = 7;
            this.btnG.Text = "G";
            this.btnG.UseVisualStyleBackColor = true;
            // 
            // btnF
            // 
            this.btnF.Location = new System.Drawing.Point(370, 75);
            this.btnF.Name = "btnF";
            this.btnF.Size = new System.Drawing.Size(25, 23);
            this.btnF.TabIndex = 6;
            this.btnF.Text = "F";
            this.btnF.UseVisualStyleBackColor = true;
            // 
            // btnE
            // 
            this.btnE.Location = new System.Drawing.Point(339, 75);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(25, 23);
            this.btnE.TabIndex = 5;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            // 
            // btnD
            // 
            this.btnD.Location = new System.Drawing.Point(308, 75);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(25, 23);
            this.btnD.TabIndex = 4;
            this.btnD.Text = "D";
            this.btnD.UseVisualStyleBackColor = true;
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(277, 75);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(25, 23);
            this.btnC.TabIndex = 3;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            // 
            // btnB
            // 
            this.btnB.Location = new System.Drawing.Point(246, 75);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(25, 23);
            this.btnB.TabIndex = 2;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(215, 75);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(25, 23);
            this.btnA.TabIndex = 1;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(200, 63);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(381, 108);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 387);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(634, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(69, 17);
            this.toolStripStatusLabel1.Text = "Mensagem:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(42, 35);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnM
            // 
            this.btnM.Location = new System.Drawing.Point(246, 104);
            this.btnM.Name = "btnM";
            this.btnM.Size = new System.Drawing.Size(25, 23);
            this.btnM.TabIndex = 23;
            this.btnM.Text = "M";
            this.btnM.UseVisualStyleBackColor = true;
            // 
            // btnU
            // 
            this.btnU.Location = new System.Drawing.Point(494, 104);
            this.btnU.Name = "btnU";
            this.btnU.Size = new System.Drawing.Size(25, 23);
            this.btnU.TabIndex = 24;
            this.btnU.Text = "U";
            this.btnU.UseVisualStyleBackColor = true;
            // 
            // btnV
            // 
            this.btnV.Location = new System.Drawing.Point(215, 133);
            this.btnV.Name = "btnV";
            this.btnV.Size = new System.Drawing.Size(25, 23);
            this.btnV.TabIndex = 25;
            this.btnV.Text = "V";
            this.btnV.UseVisualStyleBackColor = true;
            // 
            // btnW
            // 
            this.btnW.Location = new System.Drawing.Point(246, 133);
            this.btnW.Name = "btnW";
            this.btnW.Size = new System.Drawing.Size(25, 23);
            this.btnW.TabIndex = 26;
            this.btnW.Text = "W";
            this.btnW.UseVisualStyleBackColor = true;
            // 
            // btnY
            // 
            this.btnY.Location = new System.Drawing.Point(308, 133);
            this.btnY.Name = "btnY";
            this.btnY.Size = new System.Drawing.Size(25, 23);
            this.btnY.TabIndex = 27;
            this.btnY.Text = "Y";
            this.btnY.UseVisualStyleBackColor = true;
            // 
            // FrmAlunos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(634, 409);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmAlunos";
            this.Text = "jogo da forca";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAlunos_FormClosing);
            this.Load += new System.EventHandler(this.FrmAlunos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpCadastro.ResumeLayout(false);
            this.tpCadastro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableData)).EndInit();
            this.tpForca.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        // Adding the missing event handler method for btnSair_Click to resolve CS1061.
        private void btnSair_Click(object sender, System.EventArgs e)
        {
            // Close the form when the "Sair" button is clicked.
            this.Close();
        }

    #endregion
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtRA;
    private System.Windows.Forms.TextBox txtNome;
    private System.Windows.Forms.OpenFileDialog dlgAbrir;
    private System.Windows.Forms.SaveFileDialog dlgSalvar;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnInicio;
    private System.Windows.Forms.ToolStripButton btnAnterior;
    private System.Windows.Forms.ToolStripButton btnProximo;
    private System.Windows.Forms.ToolStripButton btnFim;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnNovo;
    private System.Windows.Forms.ToolStripButton btnEditar;
    private System.Windows.Forms.ToolStripButton btnCancelar;
    private System.Windows.Forms.ToolStripButton btnExcluir;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnSair;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tpCadastro;
    private System.Windows.Forms.ToolStripButton btnBuscar;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabPage tpForca;
        private System.Windows.Forms.DataGridView tableData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Palavra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dica;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnQ;
        private System.Windows.Forms.Button btnT;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.Button btnO;
        private System.Windows.Forms.Button btnN;
        private System.Windows.Forms.Button btnL;
        private System.Windows.Forms.Button btnJ;
        private System.Windows.Forms.Button btnI;
        private System.Windows.Forms.Button btnH;
        private System.Windows.Forms.Button btnG;
        private System.Windows.Forms.Button btnF;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnK;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.Button btnY;
        private System.Windows.Forms.Button btnW;
        private System.Windows.Forms.Button btnV;
        private System.Windows.Forms.Button btnU;
        private System.Windows.Forms.Button btnM;
    }
}

