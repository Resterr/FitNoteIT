import { createContext, useState } from 'react';

const UsersContext = createContext();

function UsersProvider({ children }) {
  const [currentUser, setCurrentUser] = useState();
  const [accessToken, setAccessToken] = useState();
  const [refreshToken, setrefreshToken] = useState();
  
  return (
    <UsersContext.Provider value={{currentUser,accessToken,refreshToken}}>
      {children}
    </UsersContext.Provider>
  );
}

export { UsersContext, UsersProvider };