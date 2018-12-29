CERT_NAME="NativeCode"
CERT_DOMAIN="nativecode.com"
CERT_ENVIRONMENT="Production"
CERT_PATH=certs

mkdir -p $CERT_PATH/$CERT_ENVIRONMENT

openssl req -x509 -newkey rsa:4096 -sha256 -nodes\
    -keyout $CERT_PATH/$CERT_ENVIRONMENT/$CERT_NAME-cert.key \
    -out $CERT_PATH/$CERT_ENVIRONMENT/$CERT_NAME-cert.crt \
    -subj "/CN=$CERT_DOMAIN" \
    -days 3650 \
;

openssl pkcs12 -export \
    -out $CERT_PATH/$CERT_NAME-$CERT_ENVIRONMENT.pfx \
    -inkey $CERT_PATH/$CERT_ENVIRONMENT/$CERT_NAME-cert.key \
    -in $CERT_PATH/$CERT_ENVIRONMENT/$CERT_NAME-cert.crt \
    -certfile $CERT_PATH/$CERT_ENVIRONMENT/$CERT_NAME-cert.crt \
;
