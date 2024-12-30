namespace ScaneqCuencaBackend.Helpers
{
    public static class ResponseMessages
    {
        public static string SUCCESS = "Operación realizada correctamente";
        public static string UNKNOWN_ERROR = "Ocurrió un error desconocido durante la operación";
        public static string BUS_TRUCK_MODEL_ERROR = "El modelo ofrecido mediante la operación es inválido, solo 'truck' o 'bus' son válidos";
        public static string EXISTING_WORK_ORDER = "Ya existe una orden de trabajo con el FID y tipo proporcionados";
        public static string NON_EXISTING_WORK_ORDER = "No existe una orden de trabajo con los valores proporcionados";
        public static string ORDER_ALREADY_EXISTS = "Ya existe una orden de trabajo con los valores proporcionados";

        public static string USER_NOT_FOUND = "Usuario no encontrado";
        public static string USER_ALREADY_EXISTS = "El usuario ya existe";
        public static string USER_FOUND = "Usuario encontrado";
        public static string RESOURCE_NOT_FOUND = "El recurso no existe";
        public static string AUTH_SUCCESS = "Autenticación correcta";
        public static string UNAUTHORIZED = "Usuario sin permisos";
        public static string NOT_VALID_TOKEN = "Token invalido";
        public static string SYSTEM_ERROR = "Ha ocurrido un error interno";
        public static string ERROR_DURING_THE_REQUESTED_PROCESS = "Ha habido un error durante el proceso solicitado";
    }
}
