import * as React from "react";
import { Admin, Resource } from 'react-admin';
import { UserList } from './components/users';
import { PostList, PostEdit, PostCreate } from './components/posts';
import jsonServerProvider from 'ra-data-json-server';
import PostIcon from '@material-ui/icons/Book';
import UserIcon from '@material-ui/icons/Group';
import Dashboard from './dashboard';
import authProvider from './authProvider';
//import dataProvider from './DataProvider';
import { createMuiTheme } from '@material-ui/core/styles';
//import MyLayout from './MyLayout';

const theme = createMuiTheme({
  palette: {
    type: 'light', // Switching the dark mode on is a single property value change.
  },
});



const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');
const App = () => (
      <Admin authProvider={authProvider} dashboard={Dashboard} dataProvider={dataProvider} theme={theme}>
          <Resource name="users" list={UserList} icon={UserIcon}/>
          <Resource name="posts" list={PostList} edit={PostEdit} create={PostCreate} icon={PostIcon}/>
      </Admin>
)
export default App; 