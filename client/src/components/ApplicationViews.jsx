import { Outlet, Route, Routes } from "react-router-dom";
import Login from "./auth/Login";
import Register from "./auth/Register";
import { NavBar } from "./NavBar";
import { Container } from "react-bootstrap";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import { UserCampaigns } from "./Campaign/UserCampaigns";
import { CampaignDetails } from "./Campaign/CampaignDetails";

export const ApplicationViews = ({ loggedInUser, setLoggedInUser }) => {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <>
            <NavBar
              loggedInUser={loggedInUser}
              setLoggedInUser={setLoggedInUser}
            />
            <Container className="pt-5">
              <Outlet />
            </Container>
          </>
        }
      >
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <>Welcome</>
            </AuthorizedRoute>
          }
        />
        <Route path="campaigns">
          <Route
            index
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <UserCampaigns loggedInUser={loggedInUser} />
              </AuthorizedRoute>
            }
          />
          <Route
            path=":id"
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <CampaignDetails loggedInUser={loggedInUser} />
              </AuthorizedRoute>
            }
          />
          <Route
            path="create"
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                Create Campaign
              </AuthorizedRoute>
            }
          />
        </Route>
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
};
