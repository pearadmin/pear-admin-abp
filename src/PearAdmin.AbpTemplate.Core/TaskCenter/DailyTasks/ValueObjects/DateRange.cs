using Abp.Domain.Values;
using System;
using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks.ValueObjects
{
    public class DateRange : ValueObject
    {
        private DateRange()
        {

        }

        public DateRange(DateTime startTime, DateTime endTime)
        {
            CheckTime(startTime, endTime);

            StartTime = startTime;
            EndTime = endTime;
        }

        private void CheckTime(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException("开始时间不能大于结束时间");
            }
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public DateRange ChangeStartTime(DateTime startTime)
        {
            return new DateRange(startTime, this.EndTime);
        }

        public DateRange ChangeEndTime(DateTime endTime)
        {
            return new DateRange(this.StartTime, endTime);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartTime;
            yield return EndTime;
        }
    }
}
