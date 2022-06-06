﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Classes;

namespace CoinTradeOKX.Control
{
    public partial class ParamView : UserControl
    {
        PropertyInfo property = null;
        object target = null;
        private string name = "";
        private System.Windows.Forms.Control control = null;
        private Action<ParamView> valueChangedCallback = null;
        

        public string Depend
        {
            get;private set;
        }

        public object DependValue
        {
            get;private set;
        }

        public PropertyInfo Property
        {
            get
            {
                return this.property;
            }
        }

        public ParamView()
        {
            InitializeComponent();
        }

        public void SetOnChangedCallback(Action<ParamView> callback)
        {
            this.valueChangedCallback = callback;
        }



        public void SetProperty(Object obj, PropertyInfo property)
        {
            this.property = property;

            this.target = obj;
            BehaviorParameter attribute = property.GetCustomAttribute<BehaviorParameter>();
            this.lblName.Text = attribute.Name;
            this.name = lblName.Text;
            string intro = attribute.Intro;
           

            this.Depend = attribute.Dependent;
            this.DependValue = attribute.DependentValue;


            System.Windows.Forms.Control control = null;

            if (property.PropertyType == typeof(bool))
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Checked = (bool)property.GetValue(obj);
                this.Controls.Add(checkBox);
                control = checkBox;
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                DateTimePicker dateTimePicker = new DateTimePicker();
                dateTimePicker.Value = (DateTime)property.GetValue(obj);
                this.Controls.Add(dateTimePicker);
                dateTimePicker.ValueChanged += DateTimePicker_ValueChanged;
                control = dateTimePicker;
            }
            else if (property.PropertyType.BaseType == typeof(Enum))
            {
                IList<EnumFiled> fileds = this.GetDescriptionByEnum(property.PropertyType);

                ComboBox comboBox = new ComboBox();
                var curValue = property.GetValue(obj);
                EnumFiled sel = null;
                foreach(var s in fileds)
                {
                    var val = Enum.Parse(property.PropertyType, s.ValueName);
                    
                    if(Equals(val, curValue))
                    {
                        sel = s;
                    }
                    comboBox.Items.Add(s);
                }
                comboBox.SelectedItem = sel;
                this.Controls.Add(comboBox);
                control = comboBox;

                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                comboBox.SelectedIndexChanged += Combobox_SelectedIndexChanged;
            }
            else
            {
                TextBox text = new TextBox();
                text.Text = property.GetValue(obj).ToString();
                this.Controls.Add(text);
                text.TextChanged += Text_TextChanged;
                control = text;
            }
            
            control.Location = this.lblPos.Location;


            if (!string.IsNullOrEmpty(intro))
                this.toolTip1.SetToolTip(control, intro);
            
            this.control = control;
        }
 

        // 获取方法
        public IList<EnumFiled> GetDescriptionByEnum(Type enumType)
        {
            var fileds =  enumType.GetFields();

            List<EnumFiled> infos = new List<EnumFiled>();
          
             foreach (var f in fileds)
            {
                object[] objs = f.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs.Length > 0)    //当描述属性没有时，直接返回名称
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                    infos.Add( new EnumFiled(f.Name, descriptionAttribute.Description,enumType));
                }
            }

            return infos;
        }

        public string ValidateValues()
        {
            BehaviorParameter attribute = property.GetCustomAttribute<BehaviorParameter>();

            Type valueType = property.PropertyType;

            if (valueType == typeof(string) || valueType == typeof(bool))
            {
                return "";
            }
            else if(valueType == typeof(DateTime))
            {
                DateTimePicker dateTimePicker = this.control as DateTimePicker;
                if (dateTimePicker.Value < new DateTime(1970, 1, 1, 8,0,0))
                    return "时间不能小于1970-1-1 08:00:00";
            }
            else if(valueType.BaseType == typeof(Enum))
            {
                return "";
            }
            else
            {
                TextBox textBox = this.control as TextBox;
                string text = textBox.Text;

                double number;
                bool inputIsNumber = false;
                inputIsNumber = double.TryParse(text, out number);

                if (!inputIsNumber)
                {
                    return string.Format("{0}无效数字", name);
                }

                if (number < attribute.Min || number > attribute.Max)
                {
                    return string.Format("{0}不在有效范围内{1}-{2}", name, attribute.Min, attribute.Max);
                }
            }

            return "";
        }

        public object GetValue()
        {
            if (! string.IsNullOrEmpty( this.ValidateValues()))
            {
                return null;
            }

            BehaviorParameter attribute = property.GetCustomAttribute<BehaviorParameter>();

            Type valueType = property.PropertyType;

            if (valueType == typeof(bool))
            {
                CheckBox checkBox = control as CheckBox;
                bool value = checkBox.Checked;
                return value;
            }
            else if (valueType == typeof(string))
            {
                TextBox textBox = this.control as TextBox;
                string text = textBox.Text;
                return text;
            }
            else if (valueType == typeof(DateTime))
            {
                DateTimePicker dateTimePicker = this.control as DateTimePicker;
                return dateTimePicker.Value;
            }
            else if (valueType.BaseType == typeof(Enum))
            {
                ComboBox combobox = this.control as ComboBox;
                var sel = combobox.SelectedItem as EnumFiled;
                return Enum.Parse(sel.EnumType, sel.ValueName);
            }
            else
            {
                TextBox textBox = this.control as TextBox;
                string text = textBox.Text;

                double number = double.Parse(text);

                if (valueType == typeof(int))
                {
                     return (int)number;
                }
                else if (valueType == typeof(float))
                {
                    return (float)number;
                }
                else if (valueType == typeof(decimal))
                {
                    return (decimal)number;
                }
                else if (valueType == typeof(uint))
                {
                    return (uint)number;
                }
                else if (valueType == typeof(long))
                {
                    return (long)number;
                }
                else
                {
                     return number;
                }
            }

            //return null;
        }

        

        public void   Save()
        {
            object val = this.GetValue();

            if(val != null)
            {
                property.SetValue(this.target, val);
            }
        }

        private void Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.valueChangedCallback?.Invoke(this);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.valueChangedCallback?.Invoke(this);
        }

        private void Text_TextChanged(object sender, EventArgs e)
        {
            this.valueChangedCallback?.Invoke(this);
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            this.valueChangedCallback?.Invoke(this);
        }

        private void ParmaView_Load(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }

    public class EnumFiled
    {
        public string Description { get; set; }
        public string ValueName { get; set; }
        public Type EnumType { get; set; }

        public EnumFiled(string name,string description, Type enumType)
        {
            this.ValueName = name;
            this.Description = description;
            this.EnumType = enumType;
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
