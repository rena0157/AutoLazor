
# Simple bash script that sets up the site on the server
$githubRepo = "https://github.com/rena0157/AutoLazor.git"
echo "Clone from repo"
git clone $githubRepo

# go into the directory
cd AutoLazor/

echo "Building Docker image"

# Build the docker image
docker build . -t autolazer

echo "Runner Docker"
# run the docker image
docker run -d -p 80:80 autolazer --name autolazer
