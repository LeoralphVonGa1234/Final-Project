using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fianl_Project_bank_account
{
    public partial class Bank : Form
    {
        private List<BusBookingInfo> busBookings = new List<BusBookingInfo>();

        public Bank()
        {
            InitializeComponent();
            InitializeDataGridView();

        }
        private void InitializeDataGridView()
        {
            // Set up the DataGridView with columns
            dgvBusBookings.Columns.Add("BusNumber", "Bus Number");
            dgvBusBookings.Columns.Add("PassengerName", "Passenger Name");
            dgvBusBookings.Columns.Add("TicketPrice", "Ticket Price (MXN)");
            dgvBusBookings.Columns.Add("BusRoute", "Bus Route");
            dgvBusBookings.AllowUserToAddRows = false;
            dgvBusBookings.ReadOnly = true;
        }


        public class BusBookingInfo
        {
            public string BusNumber { get; set; }
            public string PassengerName { get; set; }
            public decimal TicketPrice { get; set; }
            public string BusRoute { get; set; }

            public BusBookingInfo(string busNumber, string passengerName, decimal ticketPrice, string busRoute)
            {
                BusNumber = busNumber;
                PassengerName = passengerName;
                TicketPrice = ticketPrice;
                BusRoute = busRoute;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Bank_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            string busNumber = txtBusNumber.Text;
            string passengerName = txtPassengerName.Text;
            decimal ticketPrice = decimal.TryParse(txtTicketPrice.Text, out decimal result) ? result : 0;
            string busRoute = txtBusRoute.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(busNumber) || string.IsNullOrWhiteSpace(passengerName) || string.IsNullOrWhiteSpace(busRoute))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Add a new booking
            BusBookingInfo newBooking = new BusBookingInfo(busNumber, passengerName, ticketPrice, busRoute);
            busBookings.Add(newBooking);

            // Refresh the DataGridView
            RefreshDataGridView();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBusBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to delete.");
                return;
            }

            // Find the selected booking
            var selectedRow = dgvBusBookings.SelectedRows[0];
            string busNumber = selectedRow.Cells["BusNumber"].Value.ToString();

            // Find and remove the booking from the list
            var bookingToDelete = busBookings.FirstOrDefault(b => b.BusNumber == busNumber);
            if (bookingToDelete != null)
            {
                busBookings.Remove(bookingToDelete);

                // Refresh the DataGridView
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Booking not found.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvBusBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to update.");
                return;
            }

            // Find the selected booking
            var selectedRow = dgvBusBookings.SelectedRows[0];
            string busNumber = selectedRow.Cells["BusNumber"].Value.ToString();

            // Find the booking in the list
            BusBookingInfo bookingToUpdate = busBookings.FirstOrDefault(b => b.BusNumber == busNumber);
            if (bookingToUpdate != null)
            {
                bookingToUpdate.PassengerName = txtPassengerName.Text;
                bookingToUpdate.TicketPrice = decimal.TryParse(txtTicketPrice.Text, out decimal result) ? result : 0;
                bookingToUpdate.BusRoute = txtBusRoute.Text;

                // Refresh the DataGridView
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Booking not found.");
            }


        }
        private void RefreshDataGridView()
        {
            dgvBusBookings.Rows.Clear();

            foreach (var booking in busBookings)
            {
                // Display ticket price as currency (Pesos, MXN)
                dgvBusBookings.Rows.Add(booking.BusNumber, booking.PassengerName,
                                          booking.TicketPrice.ToString("C2", new System.Globalization.CultureInfo("es-MX")), booking.BusRoute);
            }
        }

        private bool ValidateTicketPrice()
        {
            if (string.IsNullOrWhiteSpace(txtTicketPrice.Text))
            {
                MessageBox.Show("Ticket price cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtTicketPrice.Text, out decimal ticketPrice) || ticketPrice < 0)
            {
                MessageBox.Show("Ticket price must be a valid positive number in Pesos.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }// Helper method to refresh the DataGridView

 }
        

