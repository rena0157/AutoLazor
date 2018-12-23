
echo "Building Docker image"

# Build the docker image
docker build . -t autolazer

echo "Runner Docker"
# run the docker image
docker run -d -p 80:80 autolazer --name autolazer
