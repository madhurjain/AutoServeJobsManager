using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServeJobsManager
{
    class Invoice
    {
        private String _ServiceTag, _PrinterModel, _Problem, _Status, _WorkInProgress, _SpareParts, _Remarks;
        private String _CustomerName, _ContactPerson, _Address, _Phone, _MobileNo;

        public String MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        public String Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public String ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        public String CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private DateTime _ArrivalDate, _DeliveryDate;

        public Invoice(String ServiceTag)
        {
            this.ServiceTag = ServiceTag;
        }

        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        public DateTime ArrivalDate
        {
            get { return _ArrivalDate; }
            set { _ArrivalDate = value; }
        }

        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public String SpareParts
        {
            get { return _SpareParts; }
            set { _SpareParts = value; }
        }

        public String WorkInProgress
        {
            get { return _WorkInProgress; }
            set { _WorkInProgress = value; }
        }

        public String Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public String Problem
        {
            get { return _Problem; }
            set { _Problem = value; }
        }

        public String PrinterModel
        {
            get { return _PrinterModel; }
            set { _PrinterModel = value; }
        }

        public String ServiceTag
        {
            get { return _ServiceTag; }
            set { _ServiceTag = value; }
        }
    }
    public class ServiceTagNotFoundException : Exception
    {
        
        String _ServiceTag;

        public ServiceTagNotFoundException(String ServiceTag)
        {
            this.ServiceTag = ServiceTag;
        }

        public String ServiceTag
        {
            get { return _ServiceTag; }
            set { _ServiceTag = value; }
        }
    }
}
