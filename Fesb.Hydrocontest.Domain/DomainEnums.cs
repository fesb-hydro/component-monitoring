namespace Fesb.Hydrocontest.Domain
{
    public enum Statuses
    {
        Ok,
        Warning,
        Failure
    }

    public enum Warnings
    {
        None,
        LowVoltage,
        HighCurrent,
        ControllerOverheated,
        BatteryOverheated,
        MotorOverheated
    }

    public enum Failures
    {
        None,
        InputSignalNotPresent,
        WaitingForThrottleIdlePosition,
        PowerOffRequest,
        MemoryError,
        SettingsChange,
        MotorError,
        InternalPowerSourceError,
        HalPositionLearningProcedureCompleted,
        BatteryTemperatureSensor,
        MotorTemperatureSensor,
        Other
    }
}
