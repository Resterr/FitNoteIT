import React, {
  createContext,
  useState,
  ReactNode,
  Dispatch,
  SetStateAction,
} from "react";

export type UsersContextType = {
  currentUserFromContext: string | undefined;
  accessToken: string | undefined;
  refreshToken: string | undefined;
  setCurrentUserFromContext: Dispatch<SetStateAction<string | undefined>>;
  setAccessToken: Dispatch<SetStateAction<string | undefined>>;
  setRefreshToken: Dispatch<SetStateAction<string | undefined>>;
};

const UsersContext = createContext<UsersContextType | undefined>(undefined);

function UsersProvider({ children }: { children: ReactNode }) {
  const [currentUserFromContext, setCurrentUserFromContext] = useState<
    string | undefined
  >();
  const [accessToken, setAccessToken] = useState<string | undefined>();
  const [refreshToken, setRefreshToken] = useState<string | undefined>();

  return (
    <UsersContext.Provider
      value={{
        currentUserFromContext,
        accessToken,
        refreshToken,
        setCurrentUserFromContext,
        setAccessToken,
        setRefreshToken,
      }}
    >
      {children}
    </UsersContext.Provider>
  );
}

export { UsersContext, UsersProvider };
