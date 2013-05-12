'---------------------------------------------------------------------------------
' #AUTHOR:
'    Rheinard Korf
' #Date:
'    * 13 May 2013 (Code Clean Up)
'    * 2008 (Implemented newer features for updated modem)
'    * 2004 (Initial code written)
'---------------------------------------------------------------------------------
' #Project
'    RHEIKOTerminal (Wavecom Modem Interface)
' #Description: 
'    Testing and simulation of modem functions before implenting the code into an
'    alarm system. Functions can be extended to other implementations
' #Dependency
'    Project relies heavily on a freely distributed class, Rs232.vb, freely
'    available on the Internet. This is needed for interacting with the COM port.
'---------------------------------------------------------------------------------

Imports Microsoft.Win32
Imports System.IO
Imports System.Diagnostics
Imports System.Text

Public Class frmRHEIKOTerminal
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lstComPorts As System.Windows.Forms.ListBox
    Friend WithEvents txtIN As System.Windows.Forms.TextBox
    Friend WithEvents txtOut As System.Windows.Forms.TextBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFrequency As System.Windows.Forms.Label
    Friend WithEvents lblManufacturer As System.Windows.Forms.Label
    Friend WithEvents lblIMEI As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSoftware As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblLineStatus As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
    Friend WithEvents btnShowStatus As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnShowAlarms As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnAddAlarm As System.Windows.Forms.Button
    Friend WithEvents btnDeleteAlarm As System.Windows.Forms.Button
    Friend WithEvents lstAlarms As System.Windows.Forms.ListBox
    Friend WithEvents btnSetTimeDate As System.Windows.Forms.Button
    Friend WithEvents btnGetTimeDate As System.Windows.Forms.Button
    Friend WithEvents lblDataTime As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSMSRing As System.Windows.Forms.Button
    Friend WithEvents btnFaxRing As System.Windows.Forms.Button
    Friend WithEvents btnDataRing As System.Windows.Forms.Button
    Friend WithEvents btnVoiceRing As System.Windows.Forms.Button
    Friend WithEvents cboSMSMel As System.Windows.Forms.ComboBox
    Friend WithEvents cboFaxMel As System.Windows.Forms.ComboBox
    Friend WithEvents cboDataMel As System.Windows.Forms.ComboBox
    Friend WithEvents cboVoiceMel As System.Windows.Forms.ComboBox
    Friend WithEvents cboSMSVol As System.Windows.Forms.ComboBox
    Friend WithEvents cboFaxVol As System.Windows.Forms.ComboBox
    Friend WithEvents cboDataVol As System.Windows.Forms.ComboBox
    Friend WithEvents cboVoiceVol As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSendSMS As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents txtMSG As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.lstComPorts = New System.Windows.Forms.ListBox
        Me.txtIN = New System.Windows.Forms.TextBox
        Me.txtOut = New System.Windows.Forms.TextBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.btnDisconnect = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblFrequency = New System.Windows.Forms.Label
        Me.lblManufacturer = New System.Windows.Forms.Label
        Me.lblIMEI = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblSoftware = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblLineStatus = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.btnShowStatus = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnShowAlarms = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnAddAlarm = New System.Windows.Forms.Button
        Me.btnDeleteAlarm = New System.Windows.Forms.Button
        Me.lstAlarms = New System.Windows.Forms.ListBox
        Me.btnSetTimeDate = New System.Windows.Forms.Button
        Me.btnGetTimeDate = New System.Windows.Forms.Button
        Me.lblDataTime = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnSMSRing = New System.Windows.Forms.Button
        Me.btnFaxRing = New System.Windows.Forms.Button
        Me.btnDataRing = New System.Windows.Forms.Button
        Me.btnVoiceRing = New System.Windows.Forms.Button
        Me.cboSMSMel = New System.Windows.Forms.ComboBox
        Me.cboFaxMel = New System.Windows.Forms.ComboBox
        Me.cboDataMel = New System.Windows.Forms.ComboBox
        Me.cboVoiceMel = New System.Windows.Forms.ComboBox
        Me.cboSMSVol = New System.Windows.Forms.ComboBox
        Me.cboFaxVol = New System.Windows.Forms.ComboBox
        Me.cboDataVol = New System.Windows.Forms.ComboBox
        Me.cboVoiceVol = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnSendSMS = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtMSG = New System.Windows.Forms.TextBox
        Me.txtTel = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstComPorts
        '
        Me.lstComPorts.Location = New System.Drawing.Point(328, 88)
        Me.lstComPorts.Name = "lstComPorts"
        Me.lstComPorts.Size = New System.Drawing.Size(152, 69)
        Me.lstComPorts.TabIndex = 2
        '
        'txtIN
        '
        Me.txtIN.Location = New System.Drawing.Point(296, 264)
        Me.txtIN.Multiline = True
        Me.txtIN.Name = "txtIN"
        Me.txtIN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtIN.Size = New System.Drawing.Size(264, 96)
        Me.txtIN.TabIndex = 3
        '
        'txtOut
        '
        Me.txtOut.AcceptsReturn = True
        Me.txtOut.Location = New System.Drawing.Point(376, 368)
        Me.txtOut.Name = "txtOut"
        Me.txtOut.Size = New System.Drawing.Size(152, 20)
        Me.txtOut.TabIndex = 4
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(328, 168)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 5
        Me.btnConnect.Text = "Connect"
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(408, 168)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btnDisconnect.TabIndex = 6
        Me.btnDisconnect.Text = "Disconnect"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(304, 368)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "AT Command:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblFrequency)
        Me.GroupBox1.Controls.Add(Me.lblManufacturer)
        Me.GroupBox1.Controls.Add(Me.lblIMEI)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblSoftware)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(592, 64)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Modem Information:"
        '
        'lblFrequency
        '
        Me.lblFrequency.Location = New System.Drawing.Point(88, 38)
        Me.lblFrequency.Name = "lblFrequency"
        Me.lblFrequency.Size = New System.Drawing.Size(200, 23)
        Me.lblFrequency.TabIndex = 27
        '
        'lblManufacturer
        '
        Me.lblManufacturer.Location = New System.Drawing.Point(88, 22)
        Me.lblManufacturer.Name = "lblManufacturer"
        Me.lblManufacturer.Size = New System.Drawing.Size(200, 23)
        Me.lblManufacturer.TabIndex = 30
        '
        'lblIMEI
        '
        Me.lblIMEI.Location = New System.Drawing.Point(376, 38)
        Me.lblIMEI.Name = "lblIMEI"
        Me.lblIMEI.Size = New System.Drawing.Size(208, 23)
        Me.lblIMEI.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(323, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 23)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "IMEI:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSoftware
        '
        Me.lblSoftware.Location = New System.Drawing.Point(376, 22)
        Me.lblSoftware.Name = "lblSoftware"
        Me.lblSoftware.Size = New System.Drawing.Size(208, 23)
        Me.lblSoftware.TabIndex = 26
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(328, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Firmware:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Frequency:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 16)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Manufacturer:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLineStatus
        '
        Me.lblLineStatus.Location = New System.Drawing.Point(392, 216)
        Me.lblLineStatus.Name = "lblLineStatus"
        Me.lblLineStatus.Size = New System.Drawing.Size(104, 16)
        Me.lblLineStatus.TabIndex = 24
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(328, 216)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 16)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Line Status:"
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 5000
        '
        'btnShowStatus
        '
        Me.btnShowStatus.Location = New System.Drawing.Point(488, 208)
        Me.btnShowStatus.Name = "btnShowStatus"
        Me.btnShowStatus.Size = New System.Drawing.Size(72, 24)
        Me.btnShowStatus.TabIndex = 35
        Me.btnShowStatus.Text = "Get Status"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnShowAlarms)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.btnAddAlarm)
        Me.GroupBox2.Controls.Add(Me.btnDeleteAlarm)
        Me.GroupBox2.Controls.Add(Me.lstAlarms)
        Me.GroupBox2.Controls.Add(Me.btnSetTimeDate)
        Me.GroupBox2.Controls.Add(Me.btnGetTimeDate)
        Me.GroupBox2.Controls.Add(Me.lblDataTime)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(296, 168)
        Me.GroupBox2.TabIndex = 36
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Clock Settings"
        '
        'btnShowAlarms
        '
        Me.btnShowAlarms.Location = New System.Drawing.Point(184, 48)
        Me.btnShowAlarms.Name = "btnShowAlarms"
        Me.btnShowAlarms.Size = New System.Drawing.Size(104, 24)
        Me.btnShowAlarms.TabIndex = 43
        Me.btnShowAlarms.Text = "Show Alarms"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(160, 16)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "dd/MM/yy,hh:mm:ss, (alarm)"
        '
        'btnAddAlarm
        '
        Me.btnAddAlarm.Location = New System.Drawing.Point(184, 80)
        Me.btnAddAlarm.Name = "btnAddAlarm"
        Me.btnAddAlarm.Size = New System.Drawing.Size(104, 24)
        Me.btnAddAlarm.TabIndex = 41
        Me.btnAddAlarm.Text = "Add Alarm"
        '
        'btnDeleteAlarm
        '
        Me.btnDeleteAlarm.Location = New System.Drawing.Point(184, 112)
        Me.btnDeleteAlarm.Name = "btnDeleteAlarm"
        Me.btnDeleteAlarm.Size = New System.Drawing.Size(104, 24)
        Me.btnDeleteAlarm.TabIndex = 40
        Me.btnDeleteAlarm.Text = "Remove Selected"
        '
        'lstAlarms
        '
        Me.lstAlarms.Location = New System.Drawing.Point(8, 48)
        Me.lstAlarms.Name = "lstAlarms"
        Me.lstAlarms.Size = New System.Drawing.Size(168, 95)
        Me.lstAlarms.TabIndex = 39
        '
        'btnSetTimeDate
        '
        Me.btnSetTimeDate.Location = New System.Drawing.Point(232, 16)
        Me.btnSetTimeDate.Name = "btnSetTimeDate"
        Me.btnSetTimeDate.Size = New System.Drawing.Size(56, 24)
        Me.btnSetTimeDate.TabIndex = 38
        Me.btnSetTimeDate.Text = "Set Now"
        '
        'btnGetTimeDate
        '
        Me.btnGetTimeDate.Location = New System.Drawing.Point(184, 16)
        Me.btnGetTimeDate.Name = "btnGetTimeDate"
        Me.btnGetTimeDate.Size = New System.Drawing.Size(48, 24)
        Me.btnGetTimeDate.TabIndex = 37
        Me.btnGetTimeDate.Text = "Get"
        '
        'lblDataTime
        '
        Me.lblDataTime.Location = New System.Drawing.Point(72, 24)
        Me.lblDataTime.Name = "lblDataTime"
        Me.lblDataTime.Size = New System.Drawing.Size(128, 16)
        Me.lblDataTime.TabIndex = 36
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Date/Time:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnSMSRing)
        Me.GroupBox3.Controls.Add(Me.btnFaxRing)
        Me.GroupBox3.Controls.Add(Me.btnDataRing)
        Me.GroupBox3.Controls.Add(Me.btnVoiceRing)
        Me.GroupBox3.Controls.Add(Me.cboSMSMel)
        Me.GroupBox3.Controls.Add(Me.cboFaxMel)
        Me.GroupBox3.Controls.Add(Me.cboDataMel)
        Me.GroupBox3.Controls.Add(Me.cboVoiceMel)
        Me.GroupBox3.Controls.Add(Me.cboSMSVol)
        Me.GroupBox3.Controls.Add(Me.cboFaxVol)
        Me.GroupBox3.Controls.Add(Me.cboDataVol)
        Me.GroupBox3.Controls.Add(Me.cboVoiceVol)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 256)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(225, 135)
        Me.GroupBox3.TabIndex = 54
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Voice/Ringer Settings"
        '
        'btnSMSRing
        '
        Me.btnSMSRing.Location = New System.Drawing.Point(158, 97)
        Me.btnSMSRing.Name = "btnSMSRing"
        Me.btnSMSRing.Size = New System.Drawing.Size(52, 21)
        Me.btnSMSRing.TabIndex = 70
        Me.btnSMSRing.Text = "Set"
        '
        'btnFaxRing
        '
        Me.btnFaxRing.Location = New System.Drawing.Point(158, 76)
        Me.btnFaxRing.Name = "btnFaxRing"
        Me.btnFaxRing.Size = New System.Drawing.Size(52, 21)
        Me.btnFaxRing.TabIndex = 69
        Me.btnFaxRing.Text = "Set"
        '
        'btnDataRing
        '
        Me.btnDataRing.Location = New System.Drawing.Point(158, 55)
        Me.btnDataRing.Name = "btnDataRing"
        Me.btnDataRing.Size = New System.Drawing.Size(52, 21)
        Me.btnDataRing.TabIndex = 68
        Me.btnDataRing.Text = "Set"
        '
        'btnVoiceRing
        '
        Me.btnVoiceRing.Location = New System.Drawing.Point(158, 34)
        Me.btnVoiceRing.Name = "btnVoiceRing"
        Me.btnVoiceRing.Size = New System.Drawing.Size(52, 21)
        Me.btnVoiceRing.TabIndex = 67
        Me.btnVoiceRing.Text = "Set"
        '
        'cboSMSMel
        '
        Me.cboSMSMel.Items.AddRange(New Object() {"0", "1", "2"})
        Me.cboSMSMel.Location = New System.Drawing.Point(106, 97)
        Me.cboSMSMel.Name = "cboSMSMel"
        Me.cboSMSMel.Size = New System.Drawing.Size(52, 21)
        Me.cboSMSMel.TabIndex = 66
        Me.cboSMSMel.Text = "1"
        '
        'cboFaxMel
        '
        Me.cboFaxMel.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboFaxMel.Location = New System.Drawing.Point(106, 76)
        Me.cboFaxMel.Name = "cboFaxMel"
        Me.cboFaxMel.Size = New System.Drawing.Size(52, 21)
        Me.cboFaxMel.TabIndex = 65
        Me.cboFaxMel.Text = "1"
        '
        'cboDataMel
        '
        Me.cboDataMel.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboDataMel.Location = New System.Drawing.Point(106, 55)
        Me.cboDataMel.Name = "cboDataMel"
        Me.cboDataMel.Size = New System.Drawing.Size(52, 21)
        Me.cboDataMel.TabIndex = 64
        Me.cboDataMel.Text = "1"
        '
        'cboVoiceMel
        '
        Me.cboVoiceMel.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboVoiceMel.Location = New System.Drawing.Point(106, 34)
        Me.cboVoiceMel.Name = "cboVoiceMel"
        Me.cboVoiceMel.Size = New System.Drawing.Size(52, 21)
        Me.cboVoiceMel.TabIndex = 63
        Me.cboVoiceMel.Text = "1"
        '
        'cboSMSVol
        '
        Me.cboSMSVol.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
        Me.cboSMSVol.Location = New System.Drawing.Point(58, 97)
        Me.cboSMSVol.Name = "cboSMSVol"
        Me.cboSMSVol.Size = New System.Drawing.Size(48, 21)
        Me.cboSMSVol.TabIndex = 62
        Me.cboSMSVol.Text = "6"
        '
        'cboFaxVol
        '
        Me.cboFaxVol.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
        Me.cboFaxVol.Location = New System.Drawing.Point(58, 76)
        Me.cboFaxVol.Name = "cboFaxVol"
        Me.cboFaxVol.Size = New System.Drawing.Size(48, 21)
        Me.cboFaxVol.TabIndex = 61
        Me.cboFaxVol.Text = "6"
        '
        'cboDataVol
        '
        Me.cboDataVol.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
        Me.cboDataVol.Location = New System.Drawing.Point(58, 55)
        Me.cboDataVol.Name = "cboDataVol"
        Me.cboDataVol.Size = New System.Drawing.Size(48, 21)
        Me.cboDataVol.TabIndex = 60
        Me.cboDataVol.Text = "6"
        '
        'cboVoiceVol
        '
        Me.cboVoiceVol.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
        Me.cboVoiceVol.Location = New System.Drawing.Point(58, 34)
        Me.cboVoiceVol.Name = "cboVoiceVol"
        Me.cboVoiceVol.Size = New System.Drawing.Size(48, 21)
        Me.cboVoiceVol.TabIndex = 59
        Me.cboVoiceVol.Text = "6"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(56, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(121, 16)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "Volume        Melody"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(5, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 12)
        Me.Label12.TabIndex = 57
        Me.Label12.Text = "SMS:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(5, 78)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 12)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Fax:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(5, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 12)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Data:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(5, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 12)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Voice:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Location = New System.Drawing.Point(40, 40)
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(88, 23)
        Me.btnSendSMS.TabIndex = 55
        Me.btnSendSMS.Text = "Send SMS"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtMSG)
        Me.GroupBox4.Controls.Add(Me.txtTel)
        Me.GroupBox4.Controls.Add(Me.btnSendSMS)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 400)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(592, 72)
        Me.GroupBox4.TabIndex = 56
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Send SMS"
        '
        'txtMSG
        '
        Me.txtMSG.Location = New System.Drawing.Point(184, 16)
        Me.txtMSG.Multiline = True
        Me.txtMSG.Name = "txtMSG"
        Me.txtMSG.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMSG.Size = New System.Drawing.Size(400, 48)
        Me.txtMSG.TabIndex = 57
        '
        'txtTel
        '
        Me.txtTel.Location = New System.Drawing.Point(8, 16)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(152, 20)
        Me.txtTel.TabIndex = 56
        '
        'frmRHEIKOTerminal
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(608, 477)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnShowStatus)
        Me.Controls.Add(Me.lblLineStatus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.txtOut)
        Me.Controls.Add(Me.txtIN)
        Me.Controls.Add(Me.lstComPorts)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmRHEIKOTerminal"
        Me.Text = "RHEIKOTerminal"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Com port variables."
    'The m_CommPort object is used to send and receive data to/from the Wavecom Modem.
    Private m_CommPort As New Rs232
    Private m_ActivePort As Integer = 0
    Private m_Connected As Boolean = False
#End Region

    'Keep track of the last command sent to the modem
    Private LastCommand As String

    'Full path to the registry key that holds the registered port numbers
    'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Ports

    Private Sub frmRHEIKOTerminal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim regLocalMachine As RegistryKey = Registry.LocalMachine
        Dim regword As RegistryKey = regLocalMachine.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\Ports")
        Dim s() As String = regword.GetValueNames
        Dim i As Integer
        For i = 0 To s.Length - 1
            If InStr(s(i), "COM") > 0 Then
                lstComPorts.Items.Add(s(i))
            End If
        Next
        regword.Close()
        regLocalMachine.Close()
        'AddHandler m_CommPort.DataReceived, AddressOf CommDataRecieved
    End Sub

    'Set the active port upon selection
    Private Sub lstComPorts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstComPorts.SelectedIndexChanged
        m_ActivePort = lstComPorts.SelectedIndex + 1
    End Sub

    'Process the input a key at a time. There are some AT commands that are best executed as typed
    Private Sub txtOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOut.KeyDown
        If (e.KeyCode = Keys.Return) And m_Connected Then
            Try
                Dim st As String = ""
                LastCommand = txtOut.Text
                ' Write an AT Command to the Port.
                m_CommPort.Write(Encoding.ASCII.GetBytes(txtOut.Text & Chr(13)))
                ' Sleep long enough for the modem to respond.
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Try
                    ' As long as there is information, read one byte at a time and 
                    '   output it.
                    While (m_CommPort.Read(1) <> -1)
                        'Dim i As Integer
                        st &= Chr(m_CommPort.InputStream(0))
                    End While
                Catch exc As Exception
                    ' An exception is raised when there is no information to read.
                    '   Don't do anything here, just let the exception go.
                Finally
                    DecodeMessage(st)
                End Try
                txtOut.Text = ""
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Try
            m_CommPort.Open(m_ActivePort, 9600, 8, Rs232.DataParity.Parity_None, _
                Rs232.DataStopBit.StopBit_1, 4096)
            ' If it makes it to here, then the Comm Port is available.
            'm_CommPort.Close()
            Me.Text = "Connected."
            m_Connected = True
            GetModemInformation()
        Catch
            ' If it gets here, then the attempt to open the Comm Port
            '   was unsuccessful.
            Me.Text = "Disconnected. (Connection Failed.)"
            m_Connected = False
        End Try
    End Sub

    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
        m_CommPort.Close()
        Me.Text = "Disconnected."
    End Sub

    'Hook that receives all the incomming messages.
    Private Sub WriteMessage(ByVal message As String, ByVal linefeed As Boolean)
        Me.txtIN.Text += message
        If linefeed Then
            Me.txtIN.Text += vbCrLf
        End If
    End Sub

    'Process the incomming messages. Flow control depends on the command returned.
    Private Sub DecodeMessage(ByVal msg As String)
        Dim i As Integer

        Dim s As String = Replace(msg, vbCrLf, " ", 1, msg.Length)
        s = Trim(Replace(s, Chr(13), " "))

        If InStr(s, "CPAS") = 0 Then
            WriteMessage(s, True)
        End If
        s = Trim(Replace(s, LastCommand, ""))
        If InStr(s, "OK") Then
            s = Replace(s, "OK", "")
            Select Case UCase(LastCommand)

                '-----------------------------------------------------------------------
                '  MODEM INFORMATION
                '-----------------------------------------------------------------------
                Case "AT+CGMI" : lblManufacturer.Text = s
                Case "AT+CGMM"
                    Dim a() As String = s.Split(" ")
                    lblFrequency.Text = ""
                    For i = 0 To a.Length - 1
                        If UCase(a(i)) <> "MULTIBAND" And Trim(a(i)).Length <> 0 Then
                            lblFrequency.Text &= a(i) & "; "
                        End If
                    Next
                Case "AT+CGMR"
                    Dim a() As String = s.Split(" ")
                    Dim version, sdate As String
                    version = a(0).Substring(0, InStr(a(0), "_") - 1)
                    version = "Release " & version.Substring(0, version.Length - 2) & "." & version.Substring(version.Length - 2, 2)
                    sdate = "Generated " & New Date(a(2).Substring(4, 2) + 2000, a(2).Substring(0, 2), a(2).Substring(2, 2)).ToShortDateString
                    lblSoftware.Text = version & "; " & sdate
                Case "AT+CGSN" : lblIMEI.Text = s
                    '-----------------------------------------------------------------------
                    '
                    '-----------------------------------------------------------------------
                Case "AT+CPAS" 'Line Status
                    Dim a() As String = {"READY", "UNAVAILIBLE", "UNKNOWN", "RINGER", "BUSY", "SLEEP"}
                    s = Replace(s, "+CPAS:", "")
                    lblLineStatus.Text = a(CInt(s.Trim()))
                Case "AT+CCLK?" 'Get Time
                    s = Replace(s, "+CCLK:", "")
                    s = Replace(s, Chr(34), "")
                    lblDataTime.Text = s
                Case "AT+CALA?"
                    Dim a() As String = s.Split(" ")
                    lstAlarms.Items.Clear()
                    For i = 0 To a.Length - 1
                        a(i) = Trim(Replace(a(i), "+CALA:", ""))
                        If Trim(a(i)).Length > 1 Then
                            lstAlarms.Items.Add(a(i))
                        End If
                    Next

            End Select
        End If
    End Sub

    'Automate the collection of data from the modem.
    Private Sub GetModemInformation()
        SendData("AT+CGMI") 'Manufacturer
        SendData("AT+CGMM") 'Frequencies
        SendData("AT+CGMR") 'Firmware Version
        SendData("AT+CGSN") 'IMEI (Serial Number)
        tmrUpdate.Start()
    End Sub

    'The actually sub routine that sends the data to the modem.
    Private Sub SendData(ByVal data As String)
        If m_Connected Then
            Try
                Dim st As String = ""
                LastCommand = data
                m_CommPort.Write(Encoding.ASCII.GetBytes(data & Chr(13)))
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Try
                    While (m_CommPort.Read(1) <> -1)
                        'Dim i As Integer
                        st &= Chr(m_CommPort.InputStream(0))
                    End While
                Catch
                Finally
                    DecodeMessage(st)
                End Try
            Catch
            End Try
        End If
    End Sub

    Private Sub btnGetTimeDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTimeDate.Click
        SendData("AT+CCLK?")
    End Sub

    Private Sub btnSetTimeDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTimeDate.Click
        Dim sdate As String = Format(Now, "yy/MM/dd,hh:mm:ss")
        SendData("AT+CCLK=" & Chr(34) & sdate & Chr(34))
    End Sub

    Private Sub btnShowAlarms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAlarms.Click
        SendData("AT+CALA?")
    End Sub

    Private Sub btnShowStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowStatus.Click
        SendData("AT+CPAS")
    End Sub

    Private Sub btnDeleteAlarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAlarm.Click
        If lstAlarms.Items.Count > 0 Then
            Dim i As Integer = lstAlarms.SelectedIndex
            Dim k, j As Integer
            Dim s2 As String
            Dim st As String = lstAlarms.Items(i)
            k = InStr(st, ",")
            s2 = st.Substring(k, st.Length - k)
            j = InStr(s2, ",")
            k += j
            j = CInt(st.Substring(k, st.Length - k))
            SendData("AT+CALA=" & Chr(34) & Chr(34) & "," & j)
            SendData("AT+CALA?")
        End If
    End Sub

    Private Sub btnAddAlarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAlarm.Click
        If lstAlarms.Items.Count >= 16 Then
            MsgBox("Only 16 alarms availible.")
        Else
            Dim st As String = InputBox("yy/MM/dd,hh:mm:ss   eg.: 04/08/01,12:00:00", "New Alarm Data and Time", Format(Now, "yy/MM/dd,hh:mm:ss"))
            Dim sdate As New Date(st.Substring(0, 2), st.Substring(3, 2), st.Substring(6, 2), CInt(st.Substring(9, 2)), st.Substring(12, 2), st.Substring(15, 2))
            'If Now > sdate Then MsgBox("Date/Time expired. Please enter a new date/time.")
            st.Replace(Chr(34), "")
            st = Chr(34) & st & Chr(34)
            SendData("AT+CALA=" & st)
            SendData("AT+CALA?")
        End If
    End Sub

    Private Sub btnVoiceRing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoiceRing.Click
        SendData("AT+CRMP=0," & CInt(cboVoiceVol.Text) & ",0," & CInt(cboVoiceMel.Text))
    End Sub

    Private Sub btnDataRing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataRing.Click
        SendData("AT+CRMP=1," & CInt(cboDataVol.Text) & ",0," & CInt(cboDataMel.Text))
    End Sub

    Private Sub btnFaxRing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFaxRing.Click
        SendData("AT+CRMP=2," & CInt(cboFaxVol.Text) & ",0," & CInt(cboFaxMel.Text))
    End Sub

    Private Sub btnSMSRing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSRing.Click
        SendData("AT+CRMP=3," & CInt(cboSMSVol.Text) & ",0," & CInt(cboSMSMel.Text))
    End Sub

    'Sends a text message to the given number
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
        If txtTel.Text.Length <> 0 Then
            SendData("AT+CMGS=" & Chr(34) & txtTel.Text & Chr(34))
            SendData(txtMSG.Text & Chr(26))
        Else
            MsgBox("Please Enter a Number.")
        End If
    End Sub
End Class
