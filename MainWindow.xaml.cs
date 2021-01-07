﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using PhoneBookModel;
using System.Data.Entity;

namespace Nagy_Cristina_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {

        PhoneNumbersEntities ctx;
        IQueryable<PhoneNumber> queryPhoneNumbers;
        CollectionViewSource phoneNumbersView;

        ActionState action = ActionState.Nothing;

        Binding txtPhoneNumberBinding = new Binding();
        Binding txtSubscriberBinding = new Binding();


        public MainWindow()
        {

    
            ctx = new PhoneNumbersEntities();
            queryPhoneNumbers = (from p in ctx.PhoneNumbers select p);
            txtPhoneNumberBinding.Path = new PropertyPath("Phonenum");
            txtSubscriberBinding.Path = new PropertyPath("Subscriber");
            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
            phoneNumbersView =
            ((CollectionViewSource)(this.FindResource("phoneNumbersViewSource")));
            phoneNumbersView.Source = queryPhoneNumbers.ToList();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource phoneNumbersViewSource =
            ((System.Windows.Data.CollectionViewSource)(this.FindResource("phoneNumbersViewSource")));
            phoneNumbersViewSource.View.MoveCurrentToFirst();
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            txtPhoneNumber.Text = "";
            txtSubscriber.Text = "";
            Keyboard.Focus(txtPhoneNumber);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
            Keyboard.Focus(txtPhoneNumber);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            lstPhones.IsEnabled = true;
            btnPrevious.IsEnabled = true;
            btnNext.IsEnabled = true;
            txtPhoneNumber.IsEnabled = false;
            txtSubscriber.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumber phoneNumber = null;
            if (action == ActionState.New)
            {
                try
                {

                    phoneNumber = new PhoneNumber()
                    {
                        Phonenum = txtPhoneNumber.Text.Trim(),
                        Subscriber = txtSubscriber.Text.Trim()
                        //ContractValue = txtContractValue.Text.Trim(),
                        //ContractDate = txtContractDate.Text.Trim()
                    };

                    ctx.PhoneNumbers.Add(phoneNumber);

                    ctx.SaveChangesClientWins();


                }
                catch (DataException ex)
                {

                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
               
            }
            else
                if (action == ActionState.Edit)
            {
                try
                {
                    phoneNumber = (PhoneNumber)lstPhones.SelectedItem;
                    phoneNumber.Phonenum = txtPhoneNumber.Text.Trim();
                    phoneNumber.Subscriber = txtSubscriber.Text.Trim();

                    ctx.SaveChangesClientWins();


                }
                catch (DataException ex)
                {

                    MessageBox.Show(ex.Message);
                }

                phoneNumbersView.Source = queryPhoneNumbers.ToList();

                if (phoneNumber != null)
                {
                    lstPhones.SelectedIndex = queryPhoneNumbers.ToList().FindIndex(p => p.Id == phoneNumber.Id);
                }

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                
                //txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                //txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                //txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBinding);
                //txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);
            }
            else
                if (action == ActionState.Delete)
            {
                try
                {
                    phoneNumber = (PhoneNumber)lstPhones.SelectedItem;
                    ctx.PhoneNumbers.Remove(phoneNumber);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {

                    MessageBox.Show(ex.Message);
                }
                phoneNumbersView.Source = queryPhoneNumbers.ToList();
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
              
                //txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                //txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                //txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBinding);
                //txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);
            }

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            phoneNumbersView.View.MoveCurrentToPrevious();

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            phoneNumbersView.View.MoveCurrentToNext();
        }
    }
}