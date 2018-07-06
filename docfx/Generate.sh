SOURCE_DIR=$PWD
TEMP_REPO_DIR=$PWD/../dotnet-stellar-sdk-temp-docs

echo "Generating Documentation"
docfx metadata docfx.json
docfx build docfx.json

echo "Removing temporal directory $TEMP_REPO_DIR"
rm -rf $TEMP_REPO_DIR
mkdir $TEMP_REPO_DIR

echo "Cloning the repository with the gh-pages branch"
git clone git@github.com:elucidsoft/dotnet-stellar-sdk.git --branch gh-pages $TEMP_REPO_DIR

echo "Clear repository directory"
cd $TEMP_REPO_DIR
git rm -r *

echo "Copy documentation into the repository"
cp -r $SOURCE_DIR/_site/* .

echo "Push the new docs to the remote branch"
git add --all
git commit -m "Update documentation"
git push origin gh-pages

echo "Cleaning"
cd $SOURCE_DIR
rm -rf $TEMP_REPO_DIR
