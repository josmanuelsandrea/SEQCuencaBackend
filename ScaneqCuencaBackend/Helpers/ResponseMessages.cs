namespace ScaneqCuencaBackend.Helpers
{
    public static class ResponseMessages
    {
        public static string SUCCESS = "Operación realizada correctamente";
        public static string UNKNOWN_ERROR = "Ocurrió un error desconocido durante la operación";
        public static string BUS_TRUCK_MODEL_ERROR = "El modelo ofrecido mediante la operación es inválido, solo 'truck' o 'bus' son válidos";
        public static string EXISTING_WORK_ORDER = "Ya existe una orden de trabajo con el FID y tipo proporcionados";
        public static string NON_EXISTING_WORK_ORDER = "No existe una orden de trabajo con los valores proporcionados";
    }
}
