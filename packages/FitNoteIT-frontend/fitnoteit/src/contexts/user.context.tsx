import React, {
  createContext,
  useState,
  ReactNode,
  Dispatch,
  SetStateAction,
} from "react";

export type UsersContextType = {
  currentUser2: string | undefined;
  accessToken: string | undefined;
  refreshToken: string | undefined;
  setCurrentUser2: Dispatch<SetStateAction<string | undefined>>;
  setAccessToken: Dispatch<SetStateAction<string | undefined>>;
  setRefreshToken: Dispatch<SetStateAction<string | undefined>>;
};

const UsersContext = createContext<UsersContextType | undefined>(undefined);

function UsersProvider({ children }: { children: ReactNode }) {
  const [currentUser2, setCurrentUser2] = useState<string | undefined>();
  const [accessToken, setAccessToken] = useState<string | undefined>();
  const [refreshToken, setRefreshToken] = useState<string | undefined>();

  return (
    <UsersContext.Provider
      value={{
        currentUser2,
        accessToken,
        refreshToken,
        setCurrentUser2,
        setAccessToken,
        setRefreshToken,
      }}
    >
      {children}
    </UsersContext.Provider>
  );
}

export { UsersContext, UsersProvider };
