using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeTrackingDemo.Models
{
    public enum ChangeType
    {
        Create,
        Update,
        Delete
    }
    public class Record
    { 
        public Record()
        {
            anyChanges = false;
            ChangeDate = System.DateTime.Now;
        }
        public int RecordID { get; set; }
        public string AdminID { get; set; }
        public int ExternalID { get; set; }
        public string Changes { get; private set; }
        public ChangeType ChangeType { get; private set; }
        public string RecordType { get; private set; }
        public DateTime ChangeDate { get; private set; }
        public bool anyChanges { get; private set; }
        /// <summary>
        /// Method which accepts objects as arguments, enumerates each property(field)
        /// then loops through each property to compare if the old record and new are the same
        /// For each difference found, the property name is printed along with the old and new values
        /// Used for edit operations
        /// </summary>
        /// <param name="oldEntity">Accepts object stored in DB before changes</param>
        /// <param name="newEntity">Accepts object stored in memory after changes</param>
        /// <param name="admin">Accepts string for admin ID</param>
        /// <param name="key">Accepts primary key for that object</param>
        public void CompareRecords(object oldEntity, object newEntity, string admin, int key)
        {
            AdminID = admin;
            ChangeType = ChangeType.Update;
            RecordType = oldEntity.GetType().Name;
            var builder = new StringBuilder();
            foreach (var property in oldEntity.GetType().GetProperties())
            {
                var oldRecord = property.GetValue(oldEntity, null);
                var newRecord = property.GetValue(newEntity, null);
                if (!object.Equals(oldRecord, newRecord))
                {
                    anyChanges = true;
                    builder.Append($"/{property.Name} changed. Old: {oldRecord} New: {newRecord}    ");
                }
            }
            Changes = builder.ToString();
        }
        /// <summary>
        /// Method which accepts some details when a new object is being created
        /// Creates a summary of the record type and key in the changes field
        /// </summary>
        /// <param name="admin">Accepts string for admin ID</param>
        /// <param name="newEntity">object created</param>
        /// <param name="key">Accepts primary key of that object</param>
        public void NewRecord(string admin, object newEntity, int key)
        {
            RecordType = newEntity.GetType().Name;
            AdminID = admin;
            ChangeType = ChangeType.Create;
            Changes = ($"New {RecordType} created with primary key {key}");
        }
        /// <summary>
        /// Method which accepts an object as an argument
        /// Logs all the fields of the object before deletion in changes
        /// 
        /// </summary>
        /// <param name="oldEntity">Accepts object stored in DB before deletion</param>
        /// <param name="admin">Accepts string for admin ID</param>
        /// <param name="key">Accepts primary key of object</param>
        public void DeleteRecord(object oldEntity, string admin, int key)
        {
            AdminID = admin;
            ChangeType = ChangeType.Delete;
            RecordType = oldEntity.GetType().Name; 
            var builder = new StringBuilder();
            builder.AppendLine($"{RecordType} with primary key {key} deleted");
            foreach (var property in oldEntity.GetType().GetProperties())
            {
                builder.Append($"/{property.Name}:{property.GetValue(oldEntity, null)}   ");
            }
            Changes = builder.ToString();
        }
    }
}
