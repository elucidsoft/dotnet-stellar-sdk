SOURCE_DIR=$PWD
TEMP_REPO_DIR=$PWD/../dotnet-stellar-sdk-temp-docs

GenerateDocumentation()
{
  echo "Generating Documentation"
  echo "The documentation will be generated in 'docfx/_site'"
  docfx metadata docfx.json
  docfx build docfx.json

  echo
  echo "-------------------------"
  echo
  read -p "Press any key to exit... " -n1 -s
}

UploadDocumentation()
{
  echo "Upload Documentation"
  echo
  echo "Write the name of the branch where you want to upload it."
  read BRANCH_NAME
  echo "Starting to upload to " $BRANCH_NAME "..."

  echo "Removing temporal directory $TEMP_REPO_DIR"
  rm -rf $TEMP_REPO_DIR
  mkdir $TEMP_REPO_DIR

  echo "Cloning the repository with the gh-pages branch"
  git clone git@github.com:elucidsoft/dotnet-stellar-sdk.git --branch gh-pages $TEMP_REPO_DIR

  echo "Move to the cloned repository"
  cd $TEMP_REPO_DIR

  echo "Changing the branch"
  git branch $BRANCH_NAME
  git checkout $BRANCH_NAME

  echo "Clear repository directory"
  git rm -r *

  echo "Copy documentation into the repository"
  cp -r $SOURCE_DIR/_site/* .

  echo "Push the new docs to the remote branch"
  git add --all
  git commit -m "Update documentation"
  git push origin $BRANCH_NAME

  echo "Cleaning"
  cd $SOURCE_DIR
  rm -rf $TEMP_REPO_DIR

  echo
  echo "-------------------------"
  echo
  read -p "Press any key to exit... " -n1 -s
}

ShowMenu ()
{
  clear

  echo "---------------------------------------------------"
  echo
  echo "Welcome to the Stellar .Net documentation assistant"
  echo
  echo "---------------------------------------------------"
  echo
  echo "Please write the option you want:"
  echo
  echo "1 - Generate Documentation"
  echo "2 - Upload Documentation"
  echo "3 - Exit"
  echo

  while :
  do
    read OPTION
    case $OPTION in
  	1)
      echo
  		GenerateDocumentation
      break
  		;;
  	2)
      echo
  		UploadDocumentation
      break
  		;;
  	3)
      echo "See ya!"
      break
  		;;
    esac
  done
}

ShowMenu
